using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Control
{
    public class PlayerHead : MonoBehaviour
    {
        [SerializeField] Slider oxygenSlider;

        float timer = 0;
        float oxygen = 100;

        bool facingLadder = false;
        bool swimming = false;
        bool wet = false;
        bool drowning = false;

        public bool IsSwimming()
        {
            return swimming;
        }
        public bool IsWet()
        {
            return wet;
        }

        private void Update()
        {
            if (drowning)
                return;

            facingLadder = GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ladder"));

            oxygenSlider.value = oxygen;
            if (oxygenSlider.value == 100)
                oxygenSlider.gameObject.SetActive(false);

            else
                oxygenSlider.gameObject.SetActive(true);

            if (swimming)
            {
                oxygen = Mathf.Clamp(oxygen - 20 * Time.deltaTime, 0, 100);
                if (oxygen == 0)
                    drowning = true;
            }
            else if (oxygen < 100)
            {
                oxygen = Mathf.Clamp(oxygen + 40 * Time.deltaTime, 0, 100);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Water")
            {
                swimming = true;
                wet = true;
                timer = 0;
            }

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.tag == "Water")
            {
                swimming = true;
                wet = true;
                timer = 0;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.tag == "Water")
            {
                swimming = false;
                StartCoroutine(OutOfWater());
            }
        }

        IEnumerator OutOfWater()
        {
            while(true)
            {
                if (swimming)
                    break;

                timer += Time.deltaTime;
                if(timer > 0.5f)
                {
                    wet = false;
                    break;
                }

                yield return null;
            }

            
        }
        public bool IsFacingLadder()
        {
            return facingLadder;
        }
    }
}

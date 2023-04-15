
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue
{
    public class Talker : MonoBehaviour
    {
        public static bool active = false;
        [SerializeField] Texture2D picture;
        [SerializeField] GameObject dialoguePanel;
        [SerializeField] Transform dialoguePosition;


        GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public void Talk(int line)
        {
            if (active)
                return;


            dialoguePanel.transform.Find("Picture").GetComponent<RawImage>().texture = picture;
            dialoguePanel.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>().text = StorylineManager.sm.lines[line];
            dialoguePanel.SetActive(true);
            active = true;
           
            StartCoroutine(WaitToFinish());
        }

        public void Stop()
        {
            dialoguePanel.SetActive(false);
            active = false;
        }

        IEnumerator WaitToFinish()
        {
            Animator playerAnimator = player.GetComponent<Animator>();
            playerAnimator.SetTrigger("PopOut");
            yield return new WaitForSeconds(0.3f);

            foreach(AnimatorControllerParameter parameter in playerAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                    playerAnimator.SetBool(parameter.name, false);
            }
            
            player.transform.position = dialoguePosition.position;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Animator>().SetTrigger("PopIn");

            while (active)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(0))
                {
                    Stop();
                    break;
                }
                yield return null;

            }
            
        }


    }
}

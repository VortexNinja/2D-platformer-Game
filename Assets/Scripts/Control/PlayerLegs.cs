using UnityEngine;

namespace Game.Control
{
    public class PlayerLegs : MonoBehaviour
    {
        bool grounded = false;
        bool touchingLadder = false;

        public bool OnGround()
        {
            return grounded;
        }

        public bool OnLadder()
        {
            return touchingLadder;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Ground")
            {
                grounded = true;
            }

            else if (collision.transform.tag == "Ladder")
                touchingLadder = true;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.tag == "Ground")
            {
                grounded = true;
            }

            else if (collision.transform.tag == "Ladder")
                touchingLadder = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.tag == "Ground")
            {
                grounded = false;
            }

            else if (collision.transform.tag == "Ladder")
                touchingLadder = false;
        }
    }
}

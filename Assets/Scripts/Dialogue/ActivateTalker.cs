using UnityEngine;


namespace Game.Dialogue
{
    public class ActivateTalker : MonoBehaviour
    {
        [SerializeField] StorylineManager.state state;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag != "Player") return;

            if (StorylineManager.sm.currentState != state) return;

                if (StorylineManager.sm.currentState == state)
                GetComponent<Talker>().Talk(StorylineManager.sm.getLine());
            Destroy(this);
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

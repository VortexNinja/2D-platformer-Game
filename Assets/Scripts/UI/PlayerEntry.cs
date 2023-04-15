using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerEntry : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI name;
        [SerializeField] RawImage check;

        public void Setup(string _name)
        {
            name.text = _name;
            check.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }
}

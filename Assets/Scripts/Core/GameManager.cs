using Photon.Pun;
using UnityEngine;

namespace Game.Core
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] GameObject playerPrefab;
        private void Start()
        {
            if(PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-9, -2, 0), Quaternion.identity);
            }
        }
    }
}

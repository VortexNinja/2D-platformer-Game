using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Game.UI;
using UnityEngine.UI;

namespace Game.Core
{
    public class LoginManager : MonoBehaviourPunCallbacks
    {
        #region private Serializable field
        [Header("Panels")] //Search for PANELS if adding more
        [SerializeField] GameObject loginPanel;
        [SerializeField] GameObject connectionStatusPanel;
        [SerializeField] GameObject lobbyPanel;
        [Header("Lobby Panel items")]
        [SerializeField] GameObject playersVerticalLayout;
        [SerializeField] GameObject playerEntryPrefab;
        [SerializeField] Button playButton;

        #endregion


        #region Unity Methods
        private void Start()
        {
            loginPanel.SetActive(true);
            connectionStatusPanel.SetActive(false);
            lobbyPanel.SetActive(false);
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        #endregion

        #region Public Methods

       

         public void OnClick_ConnectToServer(TMP_InputField usernameInputField)
        {
            PhotonNetwork.NickName = usernameInputField.text;
            PhotonNetwork.ConnectUsingSettings();
            loginPanel.SetActive(false);
            connectionStatusPanel.SetActive(true);
        }

        public void OnClick_Logout()
        {
            PhotonNetwork.Disconnect();
        }

        public void OnPlay()
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }

        #endregion

        #region Photon Callbacks
        public override void OnConnected()
        {
            Debug.Log("Connected to the internet");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to server successfully");
            connectionStatusPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected from server: " + cause);
            //PANELS: incease if add more panels
            loginPanel.SetActive(true);
            connectionStatusPanel.SetActive(false);
            lobbyPanel.SetActive(false);
            

        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to join room " + message);
            string roomName = "World";
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 20;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined the room" + PhotonNetwork.CurrentRoom.Name + "using the nickname " + PhotonNetwork.NickName);
            foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                GameObject playerEntry = Instantiate(playerEntryPrefab, playersVerticalLayout.transform);
                playerEntry.GetComponent<PlayerEntry>().Setup(player.NickName);
                playerEntry.name = player.NickName;

                if(player.IsMasterClient)
                {
                    playButton.interactable = true;
                }
                else
                    playButton.interactable = false;
            }
        }

        public override void OnLeftRoom()
        {
            Debug.Log("I left room");
            foreach(Transform child in playersVerticalLayout.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("new player " + newPlayer.NickName + "has Joined");
            GameObject playerEntry = Instantiate(playerEntryPrefab, playersVerticalLayout.transform);
            playerEntry.GetComponent<PlayerEntry>().Setup(newPlayer.NickName);
            playerEntry.name = newPlayer.NickName;

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log(otherPlayer.NickName + "left the room");
            foreach(Transform child in playersVerticalLayout.transform)
            {
                if(child.name == otherPlayer.NickName)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }

            //To do: If master leaves someone else should be it
        }
        #endregion
    }
}

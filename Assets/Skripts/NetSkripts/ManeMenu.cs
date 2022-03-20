
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ManeMenu : MonoBehaviourPunCallbacks
{
    public InputField InputeNameRoom;
    public byte MaxPlayersServer = 6;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayersServer;
        PhotonNetwork.CreateRoom(InputeNameRoom.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(InputeNameRoom.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("RoomTest");
    }
}

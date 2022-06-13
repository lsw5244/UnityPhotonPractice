using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class NetManager : MonoBehaviourPunCallbacks
{
    public Text stateText;
    public Button startBtn;

    string gameVersion = "1";

    private void Awake()
    {
        // �α׸� �󸶳� ��� �� �� ����
        PhotonNetwork.LogLevel = PunLogLevel.Full;
                
        //PhotonNetwork.AutomaticallySyncScene: �츮 ������ �÷��̾� ���� ���� ũ�Ⱑ ����Ǵ� ������� ���� �� ���̰� �ε�� ���� �����ϰ� �ִ� ��� �÷��̾�� ���� �� ���Դϴ�. �츮�� Photon�� �����ϴ� �ſ� ���� ����� �̿��� �� �Դϴ�: PhotonNetwork.AutomaticallySyncScene �� ���� true�� �� MasterClient�� PhotonNetwork.LoadLevel()�� ȣ�� �� �� �ְ� ��� ����� �÷��̾���� ������ ������ �ڵ������� �ε� �� ���Դϴ�.
        PhotonNetwork.AutomaticallySyncScene = true;

        startBtn.interactable = false;
    }

    private void Start()
    {
        //PhotonNetwork.JoinOrCreateRoom()
        Connect();

        //Debug.Log(PhotonNetwork.CurrentLobby);
    }

    void Connect()
    {
        if(PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            // �ش� �������� photon Ŭ���̵�� ����Ǵ� ������ ( Photon Online Server�� �����ϴ� �Լ� )
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //public override void OnConnected()
    //{
    //    Debug.Log("OnConnected ȣ���");
    //}

    // Photon Online Server�� �����ϸ� ȣ��Ǵ� �ݹ� �Լ�.
    // PhotonNetwork.ConnectUsingSettings(); �Լ��� �Ϸ�Ǹ� ȣ��ȴ�.
    // �� �Լ��� ȣ��Ǿ��ٰ� �ؼ� Room�̳� Lobby�� ���� �ִ� ���� �ƴϴ�.
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster ȣ���");
        stateText.text = "Photon Server�� ���� ��";
        PhotonNetwork.JoinLobby();
    }

    // Lobby�� �����ϸ� ȣ��Ǵ� �ݹ� �Լ�.
    // PhotonNetwork.JoinLobby(); �Լ��� �Ϸ�Ǹ� ȣ��ȴ�.
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby ȣ���");
        stateText.text = "Lobby�� ���� ��";
        PhotonNetwork.JoinOrCreateRoom("Temp", new RoomOptions { MaxPlayers = 2 }, null);
        //PhotonNetwork.CreateRoom("Temp", new RoomOptions { MaxPlayers = 2 }, null);
    }

    // Room�� �����ϸ� ȣ��Ǵ� �ݹ� �Լ�.
    // PhotonNetwork.JoinRandomOrCreateRoom(); �Լ� ���� ���� Room�� �����ϸ� ȣ��ȴ�.
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom ȣ���");
        stateText.text = "Room�� ���� ��";
        startBtn.interactable = true;
    }

    // Room�� ����� ȣ��Ǵ� �ݹ� �Լ�.
    // PhotonNetwork.CreateRoom(); �Լ��� ���� Room�� �����ϸ� ȣ��ȴ�.
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom ȣ���");
        //PhotonNetwork.Disconnect();
    }

    // ������ �������� ����Ǵ� �ݹ� �Լ�.
    // PhotonNetwork.Disconnect()�� �����ϸ� ����ȴ�.
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected ȣ���");
    }

    // �� ������ �����ϸ� ����Ǵ� �ݹ� �Լ�.
    // PhotonNetwork.CreateRoom()�� ȣ���� ��, ���� �� �̸��� ������ ���� �� �� �ִ�.
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed ȣ���");
    }

    // Room ���� ������ �����ϸ� ����Ǵ� �ݹ� �Լ�.
    // PhotonNetwork.JoinRandomRoom()�� ȣ���� ��, �� �ο����� ��� ���ְų� �������� ������ ������ �� �ִ�.
    // �ٸ� ����� �� ������ ���ų�, ���� �ݾ��� �� �ִ�.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed ȣ���");
    }
}

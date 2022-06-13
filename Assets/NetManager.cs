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
        // 로그를 얼마나 출력 할 지 설정
        PhotonNetwork.LogLevel = PunLogLevel.Full;
                
        //PhotonNetwork.AutomaticallySyncScene: 우리 게임은 플레이어 수에 따라 크기가 변경되는 경기장을 갖게 될 것이고 로드된 씬은 연결하고 있는 모든 플레이어에서 동일 한 것입니다. 우리는 Photon이 제공하는 매우 편리한 기능을 이용할 것 입니다: PhotonNetwork.AutomaticallySyncScene 이 값이 true일 때 MasterClient는 PhotonNetwork.LoadLevel()을 호출 할 수 있고 모든 연결된 플레이어들은 동일한 레벨을 자동적으로 로드 할 것입니다.
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
            // 해당 버전으로 photon 클라이드로 연결되는 시작점 ( Photon Online Server에 접속하는 함수 )
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //public override void OnConnected()
    //{
    //    Debug.Log("OnConnected 호출됨");
    //}

    // Photon Online Server에 접속하면 호출되는 콜백 함수.
    // PhotonNetwork.ConnectUsingSettings(); 함수가 완료되면 호출된다.
    // 이 함수가 호출되었다고 해서 Room이나 Lobby로 들어와 있는 것은 아니다.
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster 호출됨");
        stateText.text = "Photon Server에 입장 함";
        PhotonNetwork.JoinLobby();
    }

    // Lobby에 접속하면 호출되는 콜백 함수.
    // PhotonNetwork.JoinLobby(); 함수가 완료되면 호출된다.
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby 호출됨");
        stateText.text = "Lobby에 입장 함";
        PhotonNetwork.JoinOrCreateRoom("Temp", new RoomOptions { MaxPlayers = 2 }, null);
        //PhotonNetwork.CreateRoom("Temp", new RoomOptions { MaxPlayers = 2 }, null);
    }

    // Room에 접속하면 호출되는 콜백 함수.
    // PhotonNetwork.JoinRandomOrCreateRoom(); 함수 등을 통해 Room에 참가하면 호출된다.
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom 호출됨");
        stateText.text = "Room에 입장 함";
        startBtn.interactable = true;
    }

    // Room을 만들면 호출되는 콜백 함수.
    // PhotonNetwork.CreateRoom(); 함수를 통해 Room을 생성하면 호출된다.
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom 호출됨");
        //PhotonNetwork.Disconnect();
    }

    // 연결이 끊어지면 실행되는 콜백 함수.
    // PhotonNetwork.Disconnect()가 성공하면 실행된다.
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected 호출됨");
    }

    // 방 생성에 실패하면 실행되는 콜백 함수.
    // PhotonNetwork.CreateRoom()를 호출할 때, 같은 방 이름이 있으면 실패 할 수 있다.
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed 호출됨");
    }

    // Room 랜덤 참가에 실패하면 실행되는 콜백 함수.
    // PhotonNetwork.JoinRandomRoom()를 호출할 때, 방 인원수가 모두 차있거나 존재하지 않으면 실패할 수 있다.
    // 다른 사람이 더 빠르게 들어갔거나, 방을 닫았을 수 있다.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed 호출됨");
    }
}

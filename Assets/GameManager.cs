using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public Transform leftPlayerPos;
    public Transform rightPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient == true)
        {
            GetComponent<PhotonView>().RPC("PlayerInstantiate", RpcTarget.All);
        }
    }


    [PunRPC]
    void PlayerInstantiate()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate("Player", leftPlayerPos.position, Quaternion.identity);
            Debug.Log("������ Ŭ���̾�Ʈ�� ������");
        }
        else
        {
            PhotonNetwork.Instantiate("Player", rightPlayerPos.position, Quaternion.Euler(0, 180, 0));
            Debug.Log("�Ϲ� Ŭ���̾�Ʈ�� ������");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

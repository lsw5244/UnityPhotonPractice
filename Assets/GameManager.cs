using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour, IPunObservable
{
    public Transform leftPlayerPos;
    public Transform rightPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate("Player", leftPlayerPos.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player", rightPlayerPos.position, Quaternion.identity);
        }
    }

    [PunRPC]
    void OtherPlayerInstantiate()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate("Player", leftPlayerPos.position, Quaternion.identity);
            Debug.Log("������ Ŭ���̾�Ʈ�� ������");
        }
        else
        {

        }
        {
            PhotonNetwork.Instantiate("Player", rightPlayerPos.position, Quaternion.Euler(0, 180, 0));
            Debug.Log("�Ϲ� Ŭ���̾�Ʈ�� ������");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}

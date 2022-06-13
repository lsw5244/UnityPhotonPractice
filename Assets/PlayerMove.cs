using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PlayerMove : MonoBehaviour
{
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    void Update()
    {
        if(photonView.IsMine == true)
        {
            transform.Translate(
                Input.GetAxis("Horizontal") * Time.deltaTime,
                0f,
                0f);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250f);
            }
        }
    }
}

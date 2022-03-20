using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Enabel : MonoBehaviour
{
    [SerializeField]private PhotonView view;
    private void Start()
    {
      
        if (!GetComponentInParent<PhotonView>().IsMine)
        {
            this.gameObject.SetActive(false);
            Debug.Log("��� �� �");
        }

        if (!view.IsMine)
        {
            Debug.Log("��� �� �");
            this.gameObject.SetActive(false);

        }


    }


    public void setView(PhotonView view)
    {
        this.view = view;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestMeGlaider : MonoBehaviour
{
    public PhotonView view;
    private void Awake()
    {
        if (view.IsMine)
        {
            this.gameObject.tag = "GlaiderMe";
        }
    }

}

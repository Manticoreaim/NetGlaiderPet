/* Cкрипт менаджера глайжеров и модулей игроков
 * создает выбранный глайдер на своем коиенте, а
 * Так же дублирует создание на остольных 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GlaiderManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Glaiders;
    [SerializeField] private GameObject[] Guns;
    [SerializeField] private GameObject LeftGun;
    [SerializeField] private GameObject RightGun;

    [SerializeField] private AimPlayre Aim;
    [SerializeField] private CameraTarget Camera;
    [SerializeField] private Transform GlaiderNet;


    [SerializeField] private GameObject CanvasAim;
    [SerializeField] private GameObject CanvasHP;
    [SerializeField] private GameObject CanvasHub;



    [SerializeField] private PhotonView view;

    [SerializeField] private GameObject[] Text; // тестово как заглушка***



    [SerializeField] PlayerControl playerControl;

    private bool GlaiderLive = false;
    private bool IsMe = true;

    [SerializeField] private int IdGlaider = 0;
    [SerializeField] private int IdLeftGun = 0;
    [SerializeField] private int IdRightGun = 0;


    private void Awake()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            this.enabled = false;
        }


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsMe)
        {
            if (GlaiderLive)
            {
                DestroyGlaider();
            }
            else
            {
                ButtonRunGlaider();
            }

        }
    }

    public void DestroyGlaider()
    {
        SwithUI(false);
        distroirGlaider();
        GlaiderLive = !GlaiderLive;
        view.RPC("RpcdistroirGlaider", RpcTarget.Others, view.ViewID);
        Cursor.visible = true;

        
    }



    private void SwithUI(bool Mode) // true = игра / False = Hab
    {
        CanvasAim.SetActive(Mode);
        CanvasHP.SetActive(Mode);
        CanvasHub.SetActive(!Mode);
    }

    private void SetGlaider()
    {

        GlaiderNet.localPosition = Vector3.zero;
        GlaiderNet.rotation = Quaternion.identity;

        GameObject GlaiderSeting = Instantiate(Glaiders[IdGlaider], GlaiderNet);

        GlaiderSeting.transform.SetParent(GlaiderNet);

        

        InterfaisGlaider InterfaisMeGlaider = GetComponentInChildren<InterfaisGlaider>();
        
        Aim.SetInterfaisGlaider(InterfaisMeGlaider);
        playerControl.SetInterfaisGlaider(InterfaisMeGlaider);

        GetComponentInChildren<TargetCamera>().GiveTargetForCamera(Camera);

        GunSetPoint[] GunTransform = GetComponentsInChildren<GunSetPoint>();


        SetGunForGlaider(Guns[IdLeftGun], LeftGun, GunTransform[0].transform, GlaiderSeting);
        SetGunForGlaider(Guns[IdRightGun], RightGun, GunTransform[1].transform, GlaiderSeting);



        SetUiGlaiderHP();

        GlaiderSeting.GetComponentInChildren<CenterGlaider>().GiveGlaiderMAneger(this);
    }

    private void SetGunForGlaider(GameObject Gun, GameObject GunView, Transform Gumball, GameObject GunInstantiate)
    {
        GunInstantiate = Instantiate(Gun, GunView.transform);
        GunInstantiate.transform.SetParent(GunView.transform);

        GunView.transform.SetParent(Gumball);

        GunView.transform.localPosition = Vector3.zero;
        GunView.transform.localRotation = Quaternion.identity;
    }




    private void SetUiGlaiderHP() // тестово как заглушка***
    {
        GetComponentInChildren<GlaiderHP>().SetText(Text);
    }

    private void distroirGlaider()
    {
        Aim.DestroeGladier();
        Camera.DestroeGladier();
        playerControl.DestroeGladier();

        LeftGun.transform.SetParent(GlaiderNet);
        RightGun.transform.SetParent(GlaiderNet);

        Destroy(LeftGun.GetComponentInChildren<ControllerCannon>().gameObject);
        Destroy(RightGun.GetComponentInChildren<ControllerCannon>().gameObject);

        Destroy(GetComponentInChildren<CenterGlaider>().gameObject);
    }


    [PunRPC]
    public void RpcSetGlaider(int GlaiderPlayerID, int viewGlaider ,int IdLeftGunView, int IdRightGunView)
    {
        if (viewGlaider == view.ViewID)
        {
            GlaiderNet.localPosition = Vector3.zero;
            GlaiderNet.rotation = Quaternion.identity;
            GameObject GlaiderSeting = Instantiate(Glaiders[GlaiderPlayerID], GlaiderNet);

            GlaiderSeting.transform.SetParent(GlaiderNet);


            GunSetPoint[] GunTransform = GetComponentsInChildren<GunSetPoint>();
            SetGunForGlaider(Guns[IdLeftGunView], LeftGun, GunTransform[0].transform, GlaiderSeting);
            SetGunForGlaider(Guns[IdRightGunView], RightGun, GunTransform[1].transform, GlaiderSeting);
        }

    }

    [PunRPC]
    public void RpcdistroirGlaider(int viewGlaider)
    {
        if (viewGlaider == view.ViewID)
        {
            Destroy(GetComponentInChildren<CenterGlaider>().gameObject);

            LeftGun.transform.SetParent(GlaiderNet);
            RightGun.transform.SetParent(GlaiderNet);

            Destroy(LeftGun.GetComponentInChildren<ControllerCannon>().gameObject);
            Destroy(RightGun.GetComponentInChildren<ControllerCannon>().gameObject);
        }
        
    }


    public void UiDropbutonGlaiderID(int GlaiderID)
    {
        IdGlaider = GlaiderID;
    }

    public void UiDropbutonleftGunID(int GunID)
    {
        IdLeftGun = GunID;
    }

    public void UiDropbutonRightGunID(int GunID)
    {
        IdRightGun = GunID;
    }

    public void ButtonRunGlaider()
    {
        if(!GlaiderLive)
        {
            SetGlaider();
            GlaiderLive = !GlaiderLive;
            view.RPC("RpcSetGlaider", RpcTarget.Others, IdGlaider, view.ViewID , IdLeftGun, IdRightGun);
            Cursor.visible = false;
            SwithUI(true);
        }   
    }

    public void ButtonLifeRoom()
    {
        if (!GlaiderLive)
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("ManeMenu");
        }
    }



}

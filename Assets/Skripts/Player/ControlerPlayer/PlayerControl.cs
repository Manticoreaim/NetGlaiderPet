/*—крипт конроллера игрока управл€ющий глайдером
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControl : MonoBehaviour
{
    public GameObject Player;
    private InterfaisGlaider PlayerInterfaisGlaider;
   [SerializeField] private AimPlayre AimPlayreControl;

    



    public float SpeedMouseForAim;



    private bool ControlModeInterfais = false;
    private bool ControlAktiv = false;

    private void Awake()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
        {
            this.enabled = false;
        }
    }

    void Start()
    {
        
    }




    private void FixedUpdate()
    {
        if(ControlAktiv)
        GlaiderMove();
    }

    private void GlaiderMove()
    {
        PlayerInterfaisGlaider.MoveGlaiderInterfais(new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));

        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                PlayerInterfaisGlaider.BoostGlaiderInterfais();
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                PlayerInterfaisGlaider.DashGlaiderInterfais(Input.GetAxis("Horizontal"));
        }
    }
    private void Update()
    {
        if(ControlAktiv)
        PlayerControlUpdate();
    }

    private void PlayerControlUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AimPlayreControl.ControlModeInterfais(ControlModeInterfais);
            ControlModeInterfais = !ControlModeInterfais;
        }



        Vector2 DirectionMoveMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        AimPlayreControl.AimMoveDirection(DirectionMoveMouse * SpeedMouseForAim);


        if (Input.GetButton("Fire1")) PlayerInterfaisGlaider.GunsShotInterfais();
    }


    public void SetInterfaisGlaider(InterfaisGlaider MeInterfaisGlaider)
    {
      PlayerInterfaisGlaider = MeInterfaisGlaider;
        ControlAktiv = true;

    }

    public void DestroeGladier()
    {
        ControlAktiv = false;
    }


    // при выходе из комнаты уже не нужен
    public void OnLeftRoom() 
    {
        PhotonNetwork.LoadLevel("ManeMenu");
    }


}

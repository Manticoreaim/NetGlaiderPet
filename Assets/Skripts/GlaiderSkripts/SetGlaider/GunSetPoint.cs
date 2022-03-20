using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSetPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private ControllerCannon controllerCannon;

    private bool ActivModul = false;

    void Start()
    {
        if((controllerCannon = GetComponentInChildren<ControllerCannon>()) != null )
        {
            ActivModul = true;
        }
    }

    public void GunShot()
    {
        if(ActivModul)
        {
            controllerCannon.GunShot();
        }
    }


}

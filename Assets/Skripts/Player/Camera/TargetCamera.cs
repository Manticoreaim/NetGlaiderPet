using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    private void Awake()
    {
        //Camera.main.GetComponent<CameraTarget>().SetTarget(this.transform);
    }

    public void GiveTargetForCamera(CameraTarget camera)
    {
        camera.SetTarget(this.transform);
    }
}

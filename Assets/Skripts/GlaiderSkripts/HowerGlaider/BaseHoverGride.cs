using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHowerGride : MonoBehaviour
{
    private float AngelGraund = 60;

    private protected LayerMask layerMask;


    // Процедура инициализации 
    public void initializationLayerMask(LayerMask layerMask)
    {
        this.layerMask = layerMask;
    }





    // Вункция выстрела луча для вычесления дистанции
    private  protected virtual float RaycastHoverGrid()
    {
        Debug.DrawRay(this.transform.position, -this.transform.up * 10f, Color.black, 1f);

        RaycastHit hitData;
        if(Physics.Raycast(this.transform.position, -this.transform.up, out hitData, 80, layerMask))
        {
            if(Vector3.Angle(hitData.normal, Vector3.up) < AngelGraund) return hitData.distance;
            return hitData.distance * 2;
        }
       
            return 0;
        
       
    }

}

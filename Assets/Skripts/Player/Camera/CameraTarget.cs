/* Скрпт фокуса камеры на глайдаре 
 * Принимает Transform цели и следит за ней
 * на опредиденном растоянии и без наклонов камеры,
 * Только повороты 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    
    public Transform target; // Цель
    public LayerMask maskObstacles;  // Маска слоев препятствий
    private Vector3 _position;
    public GameObject Glaider;




    private bool Mode = true; 
    private bool LookGlaiderActive = false; 

    void Start()
    {
       
    }

    void Update()
    {
        if (LookGlaiderActive) LookGlaider();
    }

    private void LookGlaider()
    {
        if (Mode)
        {
            var oldRotation = target.rotation;

            target.rotation = Quaternion.Euler(0, oldRotation.eulerAngles.y, 0);
            var currentPosition = target.TransformPoint(_position);
            target.rotation = oldRotation;

            transform.position = currentPosition;
            var currentRotation = Quaternion.LookRotation(target.position - transform.position);
            Vector3 targetTest = target.position + new Vector3(0, 3, 0);

            transform.LookAt(targetTest);

            RaycastHit hit;
            if (Physics.Raycast(target.position, transform.position - target.position, out hit, Vector3.Distance(transform.position, target.position), maskObstacles))
            {
                transform.position = hit.point;

            }

            this.transform.position += this.transform.forward * Input.GetAxis("Vertical") * -0.4f;
        }
        else
        {
            transform.position = target.TransformPoint(new Vector3(0, 0.6f, 0.45f)); // тестовый вариант от первого лица----------------------
            transform.rotation = target.transform.rotation;
        }


    }


    // Для ситевой состовляющей 

    public void CameraMode( bool Mode)
    {
        this.Mode = Mode;
    }

    public void SetTarget(Transform targetTF)
    {
        this.transform.localPosition = new Vector3(0, 3, -9);
        this.target = targetTF;
        LookGlaiderActive = true;

        _position = target.InverseTransformPoint(transform.position);
    }
    public void DestroeGladier()
    {
        LookGlaiderActive = false;
    }
}

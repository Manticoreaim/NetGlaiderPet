/* “естовый скрипт AI 
 * берет две миролвые кардинаты и преобразовывает их
 * в локальные направлени€ (векторы) после чего 
 * через интерфейс управл€ет галйдером.
 * 
 * ≈сть два направлени€: направлени€ движение и напровлние взгл€да
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItest : MonoBehaviour
{
    public float AngalOpenZone = 15;

    // Ћокальные позиции
   [SerializeField] private Transform LocalAimTransform;
   [SerializeField] private Transform LocalMoveTransform;

    // ¬нешнии позиции
   [SerializeField] private Transform TargetEnemyTransform;
   [SerializeField] private Transform TargetPointMoveTransform;


   [SerializeField] private GameObject Glaider;

    private InterfaisGlaider InterfaisGlaiderAI;

    private void Start()
    {
        InterfaisGlaiderAI = Glaider.GetComponent<InterfaisGlaider>();
    }

    private void Update()
    {
        // локальным позиции присваиваютьс€ кординаты внешних таргетов
        LocalAimTransform.position = TargetEnemyTransform.position;
        LocalMoveTransform.position = TargetPointMoveTransform.position;

        // перемещение в точку
        InterfaisGlaiderAI.MoveGlaiderInterfais(new Vector2(LocalMoveTransform.localPosition.normalized.z / 2, LocalMoveTransform.localPosition.normalized.x / 2));


        Vector3 DirectionLookAi = new Vector3(LocalAimTransform.localPosition.normalized.x, LocalAimTransform.localPosition.normalized.y, -1f);

        if (Vector3.Angle(DirectionLookAi, Vector3.back) > AngalOpenZone) // если глайдер сомтрит на таргет с отклонение свободной зоны в градусах
        {
            InterfaisGlaiderAI.DirectionsrGlaiderInterfais(DirectionLookAi, true); 
        }
        else
        {
            InterfaisGlaiderAI.DirectionsrGlaiderInterfais(DirectionLookAi, false);
            InterfaisGlaiderAI.StopTurnGlaiderInterfais();
        }

        InterfaisGlaiderAI.DirectionGunsInterfais(LocalAimTransform.position);

    }



}

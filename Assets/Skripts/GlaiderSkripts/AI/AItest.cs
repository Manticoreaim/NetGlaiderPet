/* �������� ������ AI 
 * ����� ��� �������� ��������� � ��������������� ��
 * � ��������� ����������� (�������) ����� ���� 
 * ����� ��������� ��������� ���������.
 * 
 * ���� ��� �����������: ����������� �������� � ���������� �������
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItest : MonoBehaviour
{
    public float AngalOpenZone = 15;

    // ��������� �������
   [SerializeField] private Transform LocalAimTransform;
   [SerializeField] private Transform LocalMoveTransform;

    // ������� �������
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
        // ��������� ������� �������������� ��������� ������� ��������
        LocalAimTransform.position = TargetEnemyTransform.position;
        LocalMoveTransform.position = TargetPointMoveTransform.position;

        // ����������� � �����
        InterfaisGlaiderAI.MoveGlaiderInterfais(new Vector2(LocalMoveTransform.localPosition.normalized.z / 2, LocalMoveTransform.localPosition.normalized.x / 2));


        Vector3 DirectionLookAi = new Vector3(LocalAimTransform.localPosition.normalized.x, LocalAimTransform.localPosition.normalized.y, -1f);

        if (Vector3.Angle(DirectionLookAi, Vector3.back) > AngalOpenZone) // ���� ������� ������� �� ������ � ���������� ��������� ���� � ��������
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

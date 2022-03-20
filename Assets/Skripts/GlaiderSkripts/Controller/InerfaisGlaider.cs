/* �������� ���������� ������������ ��������
 * ����� ��� �������� �������� ������ � �������
 */

using UnityEngine;

public interface InterfaisGlaider
{
    // ����������� ��������
    public void MoveGlaiderInterfais(Vector2 direction);

    // ������� � ������ �������� 
    public void DirectionsrGlaiderInterfais(Vector3 direction, bool ActiveTurnX);

    // ��������� � ����
    public void BoostGlaiderInterfais();

    //����� �������� DashGlaiderInterfais
    public void DashGlaiderInterfais(float direction);

    // ������ �������� StopTurnGlaiderInterfaisInterfais
    public void StopTurnGlaiderInterfais();

    //������������ ������ ������ (������/������ ����) ControlModeInterfaisInterfais
    public void ControlModeInterfais(bool Mode);

    //����������� ������ �� ������� DirectionGunsInterfaisInterfais
    public void DirectionGunsInterfais(Vector3 TargetPosition);

    // ������� �� ������ GunsShotInterfaisInterfais
    public void GunsShotInterfais();
}

/*������ ���������� �������� � �������� ����������� � �������
 * 
 *  ��������� ������ �� ����������� ������ � �� ��� ���������� ������ �� �������
 *  ��� �� ��������� �� ������ ����� ������, �� ������� ���� � ��������
 *  
 *  �������� ����������� � ���������� � ������� ��� ��� ������� � ��������
 *  ����� �������� ��������� ������� ��� �����
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimPlayre : MonoBehaviour
{
    public float SpeedAIForward;
    private bool ControlModeInterfaisAim = true;

    public bool ActivAIM = false;





    [SerializeField] private RectTransform OpenZone;
    [SerializeField] private RectTransform Aim;

    public GameObject Glaider;
    private InterfaisGlaider PlayerInterfaisGlaider;
    private Camera camera;
    private CameraTarget CameraTarget;




    void Start()
    {
        camera = Camera.main;

        if ((CameraTarget = camera.GetComponent<CameraTarget>()) == null)
        {
            Debug.LogError(this.name + " not has is TargetCamera...");
            this.enabled = false;
        }
        Aim = GetComponent<RectTransform>();




    }

    private void FixedUpdate()
    {
        if(ActivAIM)
        {
            ControlAim();
        }
    }


    private void ControlAim()
    {

        Vector2 Aimcoordinates = Aim.anchoredPosition;


        if (ControlModeInterfaisAim) // �� �������� ����
        {
            if (!AimXInOpenZoneX(Aimcoordinates.x)) // ������ �� ����������� � ��������� ���� �� ���������� X
            {
                Vector3 DirectionsReturneAim = Vector3.Normalize(-Aim.anchoredPosition3D);
                Aim.position = Aim.position + new Vector3(DirectionsReturneAim.x, 0, 0) * SpeedAIForward * SpeedReturniAimX() * 0.1f; // ����������� ������� � ������


                PlayerInterfaisGlaider.DirectionsrGlaiderInterfais(DirectionAim(), true); // ������� � �������� ���������
            }
            else // ������ � ��������� ����
            {
                PlayerInterfaisGlaider.DirectionsrGlaiderInterfais(DirectionAim(), false); // ������� �������� ��������
                PlayerInterfaisGlaider.StopTurnGlaiderInterfais(); // ������ ��������� 
            }
        }
        else // �� ������� ����
        {
            if (!AimXInOpenZoneX(Aimcoordinates.x) || !AimYInOpenZoneY(Aimcoordinates.y)) // ������ �� ���������� � ��������� ���� �� ���� ����������
            {
                Vector3 DirectionsReturneAim = Vector3.Normalize(-Aim.anchoredPosition3D);

                Aim.position = Aim.position + new Vector3(DirectionsReturneAim.x * SpeedReturniAimX(), DirectionsReturneAim.y * SpeedReturniAimY(), 0) * SpeedAIForward * 0.1f; // ����������� ������� � ������


                PlayerInterfaisGlaider.DirectionsrGlaiderInterfais(DirectionAim(), true); // �������� � �������� ����������
            }
            else
            {
                PlayerInterfaisGlaider.StopTurnGlaiderInterfais(); // ������ ���������
            }
        }


        Vector3 point = RayIsCameraFromAim();
        PlayerInterfaisGlaider.DirectionGunsInterfais(point); // ����������� �����
    }





    // ��� �� ������ ���������� ����� ������

    private Vector3 RayIsCameraFromAim()
    {
        Ray ray = camera.ScreenPointToRay(Aim.position); // �������� ����
        Debug.DrawRay(camera.transform.position, ray.direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // ���� ��� �������� �� �������� ��������� 
        {
            return hit.point;
        }
        else // �����, �������� ��������� ��� �� � ���� ��������� ����������� ����
        {
            return camera.transform.position + ray.direction * 10000f;
        }

    }


    // �������� �� ������� ������� � �������� ���� �� ���� ����������
    private bool AimXInOpenZoneX(float AbsAimX)
    {
        if (Mathf.Abs(AbsAimX) < OpenZone.rect.width / 2) return true;
        else return false;
    }
    private bool AimYInOpenZoneY(float AbsAimY)
    {
        if (Mathf.Abs(AbsAimY) <= OpenZone.rect.width / 2) return true;
        else return false;
    }


    // �������� ������ ������������ (������������!!)
    public void AimMoveDirection(Vector2 Direction)
    {
        AimMove(Direction);
    }
    

    // ������������ ������� �� ������
    private void AimMove(Vector2 Direction)
    {
        Vector3 DirectionAim = Direction * Time.deltaTime;

        if ((Mathf.Abs(Aim.anchoredPosition.x + DirectionAim.x) > (camera.pixelWidth / 4)))
        {
            DirectionAim.x = 0;
        }
        if ((Mathf.Abs(Aim.anchoredPosition.y + DirectionAim.y) > (camera.pixelHeight)))
        {
            DirectionAim.y = 0;
        }




        Aim.position = Aim.position + DirectionAim;
    }


    // ������ �������� ����������� �������
    private float SpeedReturniAimX()
    {
        float speedAim = Mathf.Abs(Aim.anchoredPosition.x) / (camera.pixelWidth / 4);
        if (speedAim > 1) return 1;
        return speedAim;
    }

    private float SpeedReturniAimY()
    {
        float speedAim = Mathf.Abs(Aim.anchoredPosition.y) / (camera.pixelHeight / 4);
        if (speedAim > 1) return 1;
        return speedAim;
    }


    private Vector3 DirectionAim() // ������� ������
    {
        return new Vector3(Aim.anchoredPosition3D.x, Aim.anchoredPosition3D.y, -camera.pixelHeight / 2).normalized;

    }


    // ������������ ������ ( �� ������� ��� �� ��������)
    public void ControlModeInterfais(bool Mode)
    {
        ControlModeInterfaisAim = Mode;
        PlayerInterfaisGlaider.ControlModeInterfais(Mode);
        CameraTarget.CameraMode(Mode);
    }


    // ��������� �������� ( ��� ������� �����)
    public void SetInterfaisGlaider(InterfaisGlaider MeGlaider)
    {
        PlayerInterfaisGlaider = MeGlaider;
        ActivAIM = true;
    }

    public void DestroeGladier()
    {
        ActivAIM = false;
    }





}

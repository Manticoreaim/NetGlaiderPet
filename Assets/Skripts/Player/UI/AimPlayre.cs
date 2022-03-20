/*Скрипт управление прицелом и отправки направления в глайдер
 * 
 *  ПРИНИМАЕТ данные от контроллеоа игрока и по ним перемещает прицел по экранну
 *  Так же принемает от игрока режим работы, от первого лица и третьего
 *  
 *  ОТСЫЛАЕТ направление и активность в глайдер для его наклона и поворота
 *  Также отсылает кординаты таргета для пушек
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


        if (ControlModeInterfaisAim) // От третьего лица
        {
            if (!AimXInOpenZoneX(Aimcoordinates.x)) // Прицел не находитьсмя в свободной зоне по кординатам X
            {
                Vector3 DirectionsReturneAim = Vector3.Normalize(-Aim.anchoredPosition3D);
                Aim.position = Aim.position + new Vector3(DirectionsReturneAim.x, 0, 0) * SpeedAIForward * SpeedReturniAimX() * 0.1f; // возвращение прицела к центру


                PlayerInterfaisGlaider.DirectionsrGlaiderInterfais(DirectionAim(), true); // поворот и рысканье глайдером
            }
            else // прицел в свободной зоне
            {
                PlayerInterfaisGlaider.DirectionsrGlaiderInterfais(DirectionAim(), false); // простое рысканье гладером
                PlayerInterfaisGlaider.StopTurnGlaiderInterfais(); // ручник поворотов 
            }
        }
        else // От первого лица
        {
            if (!AimXInOpenZoneX(Aimcoordinates.x) || !AimYInOpenZoneY(Aimcoordinates.y)) // прицел не находиться в свободной зоне по всем кординатам
            {
                Vector3 DirectionsReturneAim = Vector3.Normalize(-Aim.anchoredPosition3D);

                Aim.position = Aim.position + new Vector3(DirectionsReturneAim.x * SpeedReturniAimX(), DirectionsReturneAim.y * SpeedReturniAimY(), 0) * SpeedAIForward * 0.1f; // возвращение прицела к центру


                PlayerInterfaisGlaider.DirectionsrGlaiderInterfais(DirectionAim(), true); // повороты и рысканье одинаковые
            }
            else
            {
                PlayerInterfaisGlaider.StopTurnGlaiderInterfais(); // ручнтк поворотов
            }
        }


        Vector3 point = RayIsCameraFromAim();
        PlayerInterfaisGlaider.DirectionGunsInterfais(point); // направление пушек
    }





    // Луч из камеры проходящий через прицел

    private Vector3 RayIsCameraFromAim()
    {
        Ray ray = camera.ScreenPointToRay(Aim.position); // создание луча
        Debug.DrawRay(camera.transform.position, ray.direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // если луч попадает то передает кординаты 
        {
            return hit.point;
        }
        else // мажет, передает кординаты где то в неме используя направление луча
        {
            return camera.transform.position + ray.direction * 10000f;
        }

    }


    // Проверки на ноличее прицела в открытой зоне по двум кординатам
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


    // сокрытие метода передвижения (инкапсуляция!!)
    public void AimMoveDirection(Vector2 Direction)
    {
        AimMove(Direction);
    }
    

    // передвижение прицела по экрану
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


    // расчет скорости возвращение прицела
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


    private Vector3 DirectionAim() // Удалить легаси
    {
        return new Vector3(Aim.anchoredPosition3D.x, Aim.anchoredPosition3D.y, -camera.pixelHeight / 2).normalized;

    }


    // Переключение режима ( от первого или от третьего)
    public void ControlModeInterfais(bool Mode)
    {
        ControlModeInterfaisAim = Mode;
        PlayerInterfaisGlaider.ControlModeInterfais(Mode);
        CameraTarget.CameraMode(Mode);
    }


    // установка глайдера ( для ситевой части)
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

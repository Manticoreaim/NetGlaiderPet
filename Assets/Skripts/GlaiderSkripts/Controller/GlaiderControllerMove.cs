using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GlaiderControllerMove : MonoBehaviour, InterfaisGlaider
{
    public float TestOptimalMass;


    public float ForceForfardBack;
    public float ForceRightleft;
    public float SpeedRotationGlaiderY;
    public float ForceStopingTurnGlaider;
    public float ForceTurnX;
    public float SpeedTurnGuns;
    public float CooldownTimeDush = 7;
    public float CooldownTimeBust = 15;


    public float CoolDownDash;
    public float CoolDownBoost;

    public GameObject LeftGunSetup;
    public GameObject RightGunSetup;


    public Transform RightGunTrtansform;
    public Transform LeftGunTrtansform;

    public GunSetPoint GunRightControler;
    public GunSetPoint GunLeftControler;


    private float TangajeOfGlaider = 0;

    private bool ActiveDush = true;
    private bool ActiveBust = true;

    private Vector3 BaseVectore;

    




   [SerializeField] private Rigidbody RBGlaider;
    private Vector3 DirectionGlaider;

    private bool ControlModeInterfaisDirection = true;

    private void Awake()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
        {
            this.enabled = false;
        }
    }

    void Start()
    {
        BaseVectore = Vector3.back + Vector3.down; // вектор с углом под 45 градусов

        RBGlaider = GetComponentInParent<Rigidbody>();


        LeftGunTrtansform = LeftGunSetup.GetComponentInChildren<Transform>();
        GunLeftControler = LeftGunSetup.GetComponent<GunSetPoint>();

        RightGunTrtansform = RightGunSetup.GetComponentInChildren<Transform>();
        GunRightControler = RightGunSetup.GetComponent<GunSetPoint>();
    }

    void FixedUpdate()
    {
        FixSetTangajeGlaider();
    }

    /*
     * 
     * реализаци€ интерфейса
     * 
     * 
     */

    public void MoveGlaiderInterfais(Vector2 direction) 
    {
        MoveGlaider(direction);
    }
  
    public void DirectionsrGlaiderInterfais(Vector3 direction, bool ActiveTurnX)
    {
        DirectionsrGlaider(direction, ActiveTurnX);
    }
   
    public void DirectionGunsInterfais(Vector3 TargetPosition) 
    {
        DirectionGuns(TargetPosition);
    }

    public void ControlModeInterfais(bool Mode)
    {
        ControlMode(Mode);
    }
  
    public void StopTurnGlaiderInterfais()
    {
        StopTurnGlaider();
    }
   
    public void DashGlaiderInterfais(float direction)
    {
        DashGlaider(direction);
    }

    public void GunsShotInterfais()
    {
        GunsShot();
    }

    
    public void BoostGlaiderInterfais()
    {
        BoostGlaider();
    }





    /*
     * 
     * ћетоды управлени€
     * 
     * 
     */

    private void MoveGlaider(Vector2 direction)
    {
        Vector3 ForceDirection = new Vector3(this.transform.forward.x, 0, this.transform.forward.z);
        RBGlaider.AddForce(ForceDirection * direction.x * ForceForfardBack * TestOptimalMass);

        ForceDirection = new Vector3(this.transform.right.x, 0, this.transform.right.z);
        RBGlaider.AddForce(ForceDirection * direction.y * ForceRightleft * TestOptimalMass);
    }



    // Ќаправление взгл€да орудий
    private void DirectionGuns(Vector3 TargetPosition)
    {
        RightGunTrtansform.LookAt(TargetPosition, Vector3.up);
        LeftGunTrtansform.LookAt(TargetPosition, Vector3.up);
    }



    // Ќаклон глайдера
    private void DirectionsrGlaider(Vector3 direction, bool ActiveTurnX)
    {
        /*  
        * ѕринимает направление в виде вектора (0,0,-1) смотрит ровно 
        * ѕринимает пораметр активности поворота глайдера по X
        */

        Vector3 directionPost;

        direction = direction.normalized;

        if (ActiveTurnX)
        {
            directionPost = new Vector3(direction.x, 0, direction.z); ;
            RBGlaider.AddRelativeTorque(Vector3.up * TurnGlaiderFromAngel(directionPost) * ForceTurnX * TestOptimalMass / 10f);
        }
        if (ControlModeInterfaisDirection)
        {
            SetTangajeGlaider(direction);
        }
        else
        {
            directionPost = new Vector3(0, direction.y, direction.z);

            AddTangajeGlaider(directionPost);
        }

    }



    // –учник поворота
    private void StopTurnGlaider()
    {
        RBGlaider.AddTorque(Vector3.up * -ForceStopingTurnGlaider * RBGlaider.angularVelocity.y, ForceMode.Impulse);
    }



    // ”становка режима работы, от первого (false) или от третьего (true)
    private void ControlMode(bool Mode)
    {
        TangajeOfGlaider = 0;
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DirectionGlaider = Vector3.zero;
        ControlModeInterfaisDirection = Mode;
    }



    // рывки в сторонны (над сделать асинхронным через асин.код или корутины!!!)
    private void DashGlaider(float direction)
    {
        if (ActiveDush)
        {
            Vector3 ForceDirection = new Vector3(this.transform.right.x, this.transform.right.y, this.transform.right.z);

            RBGlaider.AddRelativeForce(Vector3.right * 100 * TestOptimalMass * direction, ForceMode.Impulse);

            StartCoroutine("CooldownDushTaimer");
        }
    }



    // ¬ыстрел из орудий 
    private void GunsShot()
    {
        GunRightControler.GunShot();
        GunLeftControler.GunShot();
    }



    // ускорение глайдера
    private void BoostGlaider()
    {
        if (ActiveBust)
        {
            Vector3 ForceDirection = new Vector3(this.transform.forward.x, 0, this.transform.forward.z);
            RBGlaider.AddRelativeForce(Vector3.forward * 150 * TestOptimalMass, ForceMode.Impulse);
            StartCoroutine("CooldownBoosTaimer");
        }
    }


    /*
     * 
     * ¬торична€ логика
     *
     */


    // рысканье глайдера по Y( в верх-низ)
    private void SetTangajeGlaider(Vector3 direction)
    {

        //DirectionGlaider = Vector3.Lerp(DirectionGlaider, direction, SpeedRotationGlaiderY * Time.deltaTime);

        float GoRotationGlaiderX = (Vector3.Angle(BaseVectore, direction)) - 45; // ”гал к которому стремитьс€ глайдер

        TangajeOfGlaider += 90;
        GoRotationGlaiderX += 90;


        TangajeOfGlaider = Mathf.MoveTowards(TangajeOfGlaider, GoRotationGlaiderX, 1);

        TangajeOfGlaider -= 90;
        float OldRotationGlaiderY = RBGlaider.rotation.eulerAngles.y;
        float OldRotationGlaiderZ = RBGlaider.rotation.eulerAngles.z;

        TangejeMinMax();
            RBGlaider.rotation = Quaternion.Euler(-TangajeOfGlaider, OldRotationGlaiderY, OldRotationGlaiderZ);
        
    }


    private void FixSetTangajeGlaider()
    {
        float OldRotationGlaiderY = RBGlaider.rotation.eulerAngles.y;
        float OldRotationGlaiderZ = RBGlaider.rotation.eulerAngles.z;

        //if(TangejeMinMax())
             RBGlaider.rotation = Quaternion.Euler(-TangajeOfGlaider, OldRotationGlaiderY, OldRotationGlaiderZ); // тангаж физ тела
    }



    private void TangejeMinMax() // проверка наклона на мик макс
    {
        float AngelMaxMin = 70;
        if (Mathf.Abs(TangajeOfGlaider) > AngelMaxMin)
            if(TangajeOfGlaider >= 0)
            {
                TangajeOfGlaider = AngelMaxMin;
            }
            else
            {
                TangajeOfGlaider = -AngelMaxMin;
            }
    }


    private void AddTangajeGlaider(Vector3 direction)
    {

        //DirectionGlaider = Vector3.Lerp(DirectionGlaider, direction, SpeedRotationGlaiderY * Time.deltaTime);

        float AddRotationGlaiderX = (Vector3.Angle(BaseVectore, direction) - 45) * 0.05f; // на который надо сместитьс€

        //TangajeOfGlaider += 90;
        // AddRotationGlaiderX += 90;
        //if (TangejeMinMax()) AddRotationGlaiderX = -AddRotationGlaiderX; // если выходит за пределы то инверси€
        // TangajeOfGlaider = Mathf.MoveTowards(TangajeOfGlaider, AddRotationGlaiderX, 1);
        TangajeOfGlaider += AddRotationGlaiderX;

        Debug.Log(TangajeOfGlaider);
        //TangajeOfGlaider -= 90;
        float OldRotationGlaiderY = RBGlaider.rotation.eulerAngles.y;
        float OldRotationGlaiderZ = RBGlaider.rotation.eulerAngles.z;
        TangejeMinMax();
            this.transform.localRotation = Quaternion.Euler(-TangajeOfGlaider, 0, 0); // “ангаж в нутри физ тела
    }




    // ѕоворот глайдера по задонному направлению (в зависемости заполниносмти переменной direction)

    private float TurnGlaiderFromAngel(Vector3 direction)
    {
        // direction = new Vector3(direction.x, 0, direction.z);

        float Angel = (Vector3.Angle(direction, Vector3.forward) - 10);

        if (direction.x < 0 || direction.y < 0)
        {
            Angel = -Angel;
            if (Angel < -50)
            {
                Angel = -50;
            }
                // заменить на константы
        }
        else
        {
            if (Angel > 50)
            {
                Angel = 50;
            }
        }

        return Angel;


    }


     






    /*
     * 
     * 
     * “аймеры (корутины)
     * 
     */

    private IEnumerator CooldownDushTaimer()
    {
        ActiveDush = false;
        yield return new WaitForSeconds(CooldownTimeDush);
        ActiveDush = true;
    }

    private IEnumerator CooldownBoosTaimer()
    {
        ActiveBust = false;
        yield return new WaitForSeconds(CooldownTimeBust);
        ActiveBust = true;
    }

}

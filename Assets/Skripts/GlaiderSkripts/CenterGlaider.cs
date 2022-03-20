using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CenterGlaider : MonoBehaviour
{
    public float OptimalMass; // ����������� ����� ��������
    public float MassGlaider; // ������� ����� ��������

    [SerializeField] private float ThisHeatGlaider;
    [SerializeField] private float MaxHeatGlaider;

    [SerializeField] private float This�nergyGlaider;
    [SerializeField] private float Max�nergyGlaider;



    private Hover hover;


    private CorpuseGlaider corpuseGlaider;
    private string NameModuleCorpus = "Corpuse";

    [SerializeField] private Rigidbody RBGlaider;

    [SerializeField] private Transform LeftGunSetupTransform;
    [SerializeField] private Transform RightGunSetupTransform;

    public GameObject Aim;

    private Dictionary<string, InterfaiseModule> MeModulse;
    private List<string> keylist = new List<string> {"Corpuse"};


    private void Awake()
    {
        RBGlaider = GetComponentInParent<Rigidbody>();


    }


    private void InitializeMeModulse()
    {
        MeModulse = new Dictionary<string, InterfaiseModule>();
        MeModulse.Add(corpuseGlaider.SetCenterGlaider(this), corpuseGlaider);
    }


    public void GiveGlaiderMAneger(GlaiderManager glaiderManager)
    {
        corpuseGlaider = new CorpuseGlaider();
        corpuseGlaider.SetGlaiderManager(glaiderManager, NameModuleCorpus);


        InitializeMeModulse();
    }


    public void SetModulsGlaider(GameObject leftGun, GameObject RightGun)
    {
        GameObject Gun = Instantiate(leftGun, LeftGunSetupTransform);
        Gun.transform.SetParent(LeftGunSetupTransform);

        Gun = Instantiate(RightGun, RightGunSetupTransform);
        Gun.transform.SetParent(RightGunSetupTransform);
    }

    void Start()
    {

        if ((hover = GetComponentInChildren<Hover>()) == null)
        {
            Debug.LogError(this.name + " not has Children is Hower");
            this.enabled = false;
        }
            // ���������� ������� Hover
        hover.initializationHower(MassGlaider, OptimalMass, RBGlaider); //   ������������� Hover, ������� ������ ������ �� CenterGlaider

        //InitializeMeModulse();
    }

    private void FixedUpdate()
    {

        hover.UpdateHover(); // ����� ���������� ������
    }


    private void Update()
    {

    }


    public bool AddHoteGlaider(float Hote)
    {
        return true;
    }


    public void DamageModule(string NameModule, int NewNoduleHP)
    {

        if (this.keylist.Contains(NameModule) == true)
        {
            MeModulse[NameModule].SetNewIdexDamage(NewNoduleHP);
        }
    }

    private class CorpuseGlaider : BaseModule
    {

        public GlaiderManager glaiderManager;
        public override void SetNewIdexDamage(int IndexDamage)
        {
            //Debug.Log("������!!!!");
            if (IndexDamage == 0) glaiderManager.DestroyGlaider();
        }

        public void SetGlaiderManager(GlaiderManager glaiderManager, string NameModule)
        {
            IndexDamegeModule = 4;
            this.glaiderManager = glaiderManager;
            this.NameModule = NameModule;
        }
    }



}



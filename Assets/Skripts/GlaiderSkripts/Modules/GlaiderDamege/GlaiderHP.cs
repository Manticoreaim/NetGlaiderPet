using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlaiderHP : MonoBehaviour
{
    public float HPglaider;

    public ModulHP[]  ModulesForUI;
    public Text[]  ModulesText;
    public Text UI;

    private bool ActiveUI = true;
    private float HPglaiderMax;

    [SerializeField] private string NameModule = "Corpus";

    private List<string> keylist = new List<string> {"Corpuse"};



    private Dictionary<string, Module> Modules;


    [SerializeField] private CenterGlaider MeCenterGlaider;



    Text[] CounterHPText;
    Text[] NameModulsText;


    void Start()
    {

        if (UI == null) ActiveUI = false;
        //       IntModuls();


        HPglaiderMax = HPglaider;

        this.Modules = new Dictionary<string, Module>();
        AddModule(HPglaider ,NameModule);
    }

    public void SetText(GameObject[] text)
    {
        ModulesText = new Text[text.Length];

        for(int i = 0; i < text.Length; i++)
        {
            ModulesText[i] = text[i].GetComponent<Text>();
        }
    }




    void Update()
    {
        if(ActiveUI) UI.text = HPglaider.ToString();

        if (Input.GetKeyDown(KeyCode.R))
        {
            HPglaider = HPglaiderMax;
        }


        if (ModulesText != null && ModulesForUI != null && ModulesText.Length == ModulesForUI.Length + 1)
        {
            
            ModulesText[0].text = HPglaider.ToString();
            for (int i = 0; i < ModulesForUI.Length; i++)
            {
                ModulesText[i + 1].text = ModulesForUI[i].GiveHPGlaider();
            }
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            GetDamageModele("test", 0, 300);
        }
    }

   


      
    public void GetDamageModele(string NameModele, float DamageModel, float DamageGlaider)
    {

        Debug.Log(DamageGlaider);
        int NewIndexDamage = Modules[this.NameModule].DamageModule(DamageGlaider);
        if (NewIndexDamage != -1)
        {
            SetDamegeModule(this.NameModule, NewIndexDamage);
        }

        if (this.keylist.Contains(NameModele) == true)
        {
            NewIndexDamage = Modules[NameModele].DamageModule(DamageModel);
            if (NewIndexDamage != -1)
            {
                SetDamegeModule( NameModele, NewIndexDamage);
            }
        }
        


    }


    public void AddModule( float HPModule ,string NameModele)
    {
        this.Modules.Add(NameModele, new Module(HPModule, NameModele));
    }
    public void AddModule( float HPModule, float GodeState, float NormalState, float BadState, float DestroyStae, string NameModele)
    {
        this.Modules.Add(NameModele, new Module(HPModule, GodeState, NormalState, BadState, DestroyStae,NameModele));
    }



    private void SetDamegeModule(string NameModule, int NewNoduleHP)
    {
        MeCenterGlaider.DamageModule(NameModule, NewNoduleHP);
    }



}


public class Module
{
    string Name { get; set; }
    public float MaxHP { get; set; }
    public float ThisHP { get; set; }
    public float HPGoodState { get; set; }
    public float HPNormalState { get; set; }
    public float HPbadState { get; set; }
    public float HPDestroyState { get; set; }
    public int IndexStateModule { get; set; }

    public Module(float HP, float GoodStat, float NormalStat, float BadStat, float DestroyStat, string NameModule)
    {
        this.Name = NameModule;
        this.ThisHP = this.MaxHP = HP;
        this.HPGoodState = GoodStat;
        this.HPNormalState = NormalStat;
        this.HPbadState = BadStat;
        this.HPDestroyState = DestroyStat;
        this.IndexStateModule = 4;
    }

    public Module(float HP, string NameModule)
    {
        this.Name = NameModule;
        this.ThisHP = this.MaxHP = HP;
        this.HPGoodState = HP;
        this.HPNormalState = HP;
        this.HPbadState = HP;
        this.HPDestroyState = 0;
        this.IndexStateModule = 3;
    }

    public int DamageModule(float Damage)
    {
        ThisHP = ThisHP - Damage;
        int NewIndexState = IndexStateModule;
        if (ThisHP > HPGoodState) NewIndexState = 3;
        else if (ThisHP > HPNormalState) NewIndexState = 2;
        else if (ThisHP > HPbadState) NewIndexState = 1;
        else if (ThisHP < HPDestroyState) NewIndexState = 0;

        Debug.Log(ThisHP);
        Debug.Log(IndexStateModule);
        if (NewIndexState != IndexStateModule)
        {
            Debug.Log(ThisHP);
            IndexStateModule = NewIndexState;
            return IndexStateModule;
        }
        else
        {
            return -1;
        }


        
    }
}


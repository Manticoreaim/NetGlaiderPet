using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hover : MonoBehaviour
{

    /* Настройка через инпектор Unity */
    [SerializeField] private LayerMask layerMask;   // Маска слоев для Raycast (дабы луч проходил через не нужные объекты
    [SerializeField] private float HightHover;      // Высота покоя ховера 
    [SerializeField] private float ForceHover;     // Множитель силы ховера (при 1 силы будет хвотать только для удержания ховера)
    [SerializeField] private float AntigravCushionHeight; // Высота антиграв подушки
    [SerializeField] private float AngelsForHelp; // угол при котором глайдер включает хелпера
    [SerializeField] private float HelperCushionHeight;  // Высота хелпера

    private static float ForceOfGravity = 9.8f;     // Константа силы тяжести


 //   private СalculationForceUp Сalculation;

    /* Авто ввод данных*/
    private Rigidbody RBglaider;     // Физтело Глайдера
    private float Mass;              // Масса Шлайдера   
    private float OptimalMass;       // Оптимальная масса глайдера

    private HoverGride[] hoverGride; // Масив Ховер гридов
    private HelperHowerGrid[] HelperGride; //Масив хелперов (нужны от переворото)




    public void Start()
    {
        

        // Поиск Грида Ховеров
        if ((hoverGride = GetComponentsInChildren<HoverGride>()) == null)
        {
            Debug.LogError(this.name + " not has Children is HoverGride");
            this.enabled = false;
        }

        if ((HelperGride = GetComponentsInChildren<HelperHowerGrid>()) == null)
        {
            Debug.LogError(this.name + " not has Children is HelperHowerGrid");
            this.enabled = false;
        }


        // Инициализация Ховеров
        for (int i = 0; i < HelperGride.Length; i++)
        {
            HelperGride[i].initializationHowerGride(layerMask, HelperCushionHeight);
        }

        for (int i = 0; i < hoverGride.Length; i++) 
        {
            hoverGride[i].initializationHowerGride(layerMask, -this.transform.up , AntigravCushionHeight);
        }

    }

    // Процидура инициализации Hover
    public void initializationHower(float GladerMass, float OptimalMassGlaider, Rigidbody GlaiderRigudbody)
    {
        Mass = GladerMass;
        OptimalMass = OptimalMassGlaider;
        RBglaider = GlaiderRigudbody;

        SetSettingsRBGlaider();


       // Сalculation = new СalculationForceUp(OptimalMass, HightHover, ForceHover, hoverGride[0].DistansForGraund(layerMask));
    }


    // Установка параметры Физичесокго тела
    private void SetSettingsRBGlaider()
    {
        RBglaider.centerOfMass = Vector3.zero;
        RBglaider.mass = Mass;
    }


    // Процедура прикладывания силы полета (Эмитация анти-грав-подушки)
    private void АlightGlaider()
    {
        int NeedHelp = hoverGride.Length - 1;

        float ThisHight = hoverGride[0].DistansForGraund();
        float PostHight = hoverGride[0].givePostHight();
         
        // Первый ховер всегда прикладывает силу строго в наз
        RBglaider.AddForceAtPosition(Vector3.up * СalculationForceUpVoid(PostHight, ThisHight) / hoverGride.Length, hoverGride[0].transform.position);


      //  Debug.Log("interpritHoverGride [hoverGride 0] - " + ThisHaght);


        for (int i = 1; i < hoverGride.Length; i++)
        {
            ThisHight = hoverGride[i].DistansForGraund();
            PostHight = hoverGride[i].givePostHight();
          //  Debug.Log("interpritHoverGride [hoverGride "+ i +  "] - " + ThisHaght);
            // Последующии ховеры прикладывают силу относительно своего наклона
           // if (hoverGride[i].giveThisHight() >= AntigravCushionHeight) NeedHelp = NeedHelp-1;

            RBglaider.AddForceAtPosition(hoverGride[i].transform.up *  hoverGride[i].GiveAngelHoverGride() * СalculationForceUpVoid(PostHight, ThisHight) / hoverGride.Length, hoverGride[i].transform.position);

        }


       /* 
        if (NeedHelp <= 0 && Mathf.Abs( RBglaider.transform.eulerAngles.z) > 80)
        {
            for (int i = 1; i < HelperGride.Length; i++)
            {


                // Последующии ховеры прикладывают силу относительно своего наклона
                if (ThisHaght >= AntigravCushionHeight) NeedHelp--;

                RBglaider.AddForceAtPosition(HelperGride[i].transform.up  * OptimalMass /3 * 10, HelperGride[i].transform.position);
                
            }
            RBglaider.AddForceAtPosition(Vector3.up * OptimalMass * 10, hoverGride[0].transform.position);
        }
        */
        
    }



    // Процедура обновления ховера для вышестаящих слассов
    public void UpdateHover()
    {
        АlightGlaider();


    }


    // Функция расчета силы прикладываемой силы и ее типа

    /*                
     *   =====    
     *   | | |  
     * 
     *  ~~~~~~~~~~
     * 
     */
    private float СalculationForceUpVoid(float PastHight, float ThisHight)
    {
        float Force = OptimalMass;
        if (ThisHight > HightHover)
        {
            PastHight = ThisHight;
            Force = Mathf.Pow(ForceHover, -ThisHight + HightHover) * OptimalMass;


        }
        else
        {
            if (ThisHight > PastHight)
            {


                PastHight = ThisHight;
                Force = OptimalMass + Mathf.Sqrt(HightHover - ThisHight) * OptimalMass / 10;
 
            }
            else
            {
                PastHight = ThisHight;
                Force = Mathf.Pow(ForceHover, -ThisHight + HightHover) * OptimalMass;

            }
        }

        return Force * ForceOfGravity;
    }
}



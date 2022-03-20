using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hover : MonoBehaviour
{

    /* ��������� ����� �������� Unity */
    [SerializeField] private LayerMask layerMask;   // ����� ����� ��� Raycast (���� ��� �������� ����� �� ������ �������
    [SerializeField] private float HightHover;      // ������ ����� ������ 
    [SerializeField] private float ForceHover;     // ��������� ���� ������ (��� 1 ���� ����� ������� ������ ��� ��������� ������)
    [SerializeField] private float AntigravCushionHeight; // ������ �������� �������
    [SerializeField] private float AngelsForHelp; // ���� ��� ������� ������� �������� �������
    [SerializeField] private float HelperCushionHeight;  // ������ �������

    private static float ForceOfGravity = 9.8f;     // ��������� ���� �������


 //   private �alculationForceUp �alculation;

    /* ���� ���� ������*/
    private Rigidbody RBglaider;     // ������� ��������
    private float Mass;              // ����� ��������   
    private float OptimalMass;       // ����������� ����� ��������

    private HoverGride[] hoverGride; // ����� ����� ������
    private HelperHowerGrid[] HelperGride; //����� �������� (����� �� ����������)




    public void Start()
    {
        

        // ����� ����� �������
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


        // ������������� �������
        for (int i = 0; i < HelperGride.Length; i++)
        {
            HelperGride[i].initializationHowerGride(layerMask, HelperCushionHeight);
        }

        for (int i = 0; i < hoverGride.Length; i++) 
        {
            hoverGride[i].initializationHowerGride(layerMask, -this.transform.up , AntigravCushionHeight);
        }

    }

    // ��������� ������������� Hover
    public void initializationHower(float GladerMass, float OptimalMassGlaider, Rigidbody GlaiderRigudbody)
    {
        Mass = GladerMass;
        OptimalMass = OptimalMassGlaider;
        RBglaider = GlaiderRigudbody;

        SetSettingsRBGlaider();


       // �alculation = new �alculationForceUp(OptimalMass, HightHover, ForceHover, hoverGride[0].DistansForGraund(layerMask));
    }


    // ��������� ��������� ����������� ����
    private void SetSettingsRBGlaider()
    {
        RBglaider.centerOfMass = Vector3.zero;
        RBglaider.mass = Mass;
    }


    // ��������� ������������� ���� ������ (�������� ����-����-�������)
    private void �lightGlaider()
    {
        int NeedHelp = hoverGride.Length - 1;

        float ThisHight = hoverGride[0].DistansForGraund();
        float PostHight = hoverGride[0].givePostHight();
         
        // ������ ����� ������ ������������ ���� ������ � ���
        RBglaider.AddForceAtPosition(Vector3.up * �alculationForceUpVoid(PostHight, ThisHight) / hoverGride.Length, hoverGride[0].transform.position);


      //  Debug.Log("interpritHoverGride [hoverGride 0] - " + ThisHaght);


        for (int i = 1; i < hoverGride.Length; i++)
        {
            ThisHight = hoverGride[i].DistansForGraund();
            PostHight = hoverGride[i].givePostHight();
          //  Debug.Log("interpritHoverGride [hoverGride "+ i +  "] - " + ThisHaght);
            // ����������� ������ ������������ ���� ������������ ������ �������
           // if (hoverGride[i].giveThisHight() >= AntigravCushionHeight) NeedHelp = NeedHelp-1;

            RBglaider.AddForceAtPosition(hoverGride[i].transform.up *  hoverGride[i].GiveAngelHoverGride() * �alculationForceUpVoid(PostHight, ThisHight) / hoverGride.Length, hoverGride[i].transform.position);

        }


       /* 
        if (NeedHelp <= 0 && Mathf.Abs( RBglaider.transform.eulerAngles.z) > 80)
        {
            for (int i = 1; i < HelperGride.Length; i++)
            {


                // ����������� ������ ������������ ���� ������������ ������ �������
                if (ThisHaght >= AntigravCushionHeight) NeedHelp--;

                RBglaider.AddForceAtPosition(HelperGride[i].transform.up  * OptimalMass /3 * 10, HelperGride[i].transform.position);
                
            }
            RBglaider.AddForceAtPosition(Vector3.up * OptimalMass * 10, hoverGride[0].transform.position);
        }
        */
        
    }



    // ��������� ���������� ������ ��� ����������� �������
    public void UpdateHover()
    {
        �lightGlaider();


    }


    // ������� ������� ���� �������������� ���� � �� ����

    /*                
     *   =====    
     *   | | |  
     * 
     *  ~~~~~~~~~~
     * 
     */
    private float �alculationForceUpVoid(float PastHight, float ThisHight)
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



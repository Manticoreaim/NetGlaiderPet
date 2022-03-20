using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverGride : BasicHowerGride
{
    private float ThisHight; // ������� ������  
    private float PastHight; // ������� ������
    private float AntigravCushionHeight;

    private float InclineHoverGride;




    public  void initializationHowerGride(LayerMask layerMask, Vector3 downwardHover, float AntigravCushionHeight)
    {
        initializationLayerMask(layerMask);

        this.AntigravCushionHeight = AntigravCushionHeight;

        InclineHoverGride = Vector3.Angle(downwardHover, -this.transform.up);
       // Debug.Log("InclineHoverGride - " + InclineHoverGride);
        InclineHoverGride = (InclineHoverGride * Mathf.PI) / 180;

        PastHight = RaycastHoverGrid(); // ��������������� ������ ������� ������
    }




    // ������� �������� �������� ������
    public float DistansForGraund()
    {

        PastHight = ThisHight;      // ������ ������ ������
        ThisHight = RaycastHoverGrid();         

        if (ThisHight == 0 || ThisHight >= AntigravCushionHeight) // �������� �������� null (������ ����� ������� �� ����� ��� ����� �����������)
        {
            ThisHight = AntigravCushionHeight;
        }
         return (InterpretationAngelHight(ThisHight));  // ���� �� ��������
    }


    


    // ������������ ��������� ������ � ������ ��� ����� ������ ������ Hover  
    private float InterpretationAngelHight(float Hight)  // ���� �� ��������
    {
        return (Hight * Mathf.Cos(InclineHoverGride));
    }

    public float GiveAngelHoverGride()
    {
        return Mathf.Cos(InclineHoverGride);
    }

    // ������� ����������� ������� � ������� ������
    public float giveThisHight()
    {
        return ThisHight;
    }

    public float givePostHight()
    {
        return PastHight;
    }
}

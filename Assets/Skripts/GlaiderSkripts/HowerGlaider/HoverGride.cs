using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverGride : BasicHowerGride
{
    private float ThisHight; // Текущая высота  
    private float PastHight; // Прошлая высата
    private float AntigravCushionHeight;

    private float InclineHoverGride;




    public  void initializationHowerGride(LayerMask layerMask, Vector3 downwardHover, float AntigravCushionHeight)
    {
        initializationLayerMask(layerMask);

        this.AntigravCushionHeight = AntigravCushionHeight;

        InclineHoverGride = Vector3.Angle(downwardHover, -this.transform.up);
       // Debug.Log("InclineHoverGride - " + InclineHoverGride);
        InclineHoverGride = (InclineHoverGride * Mathf.PI) / 180;

        PastHight = RaycastHoverGrid(); // Предварительная запись прошлой высоты
    }




    // Функция передачи ненешней высоты
    public float DistansForGraund()
    {

        PastHight = ThisHight;      // запесь старой высоты
        ThisHight = RaycastHoverGrid();         

        if (ThisHight == 0 || ThisHight >= AntigravCushionHeight) // проверка значения null (пример когда глайдер не имеет под собой поверхность)
        {
            ThisHight = AntigravCushionHeight;
        }
         return (InterpretationAngelHight(ThisHight));  // пока не рабоатет
    }


    


    // Инерпритация наклонной высоты в прямую для более лучшей работы Hover  
    private float InterpretationAngelHight(float Hight)  // пока не работает
    {
        return (Hight * Mathf.Cos(InclineHoverGride));
    }

    public float GiveAngelHoverGride()
    {
        return Mathf.Cos(InclineHoverGride);
    }

    // Функции возвращения прошлой и текущей высоты
    public float giveThisHight()
    {
        return ThisHight;
    }

    public float givePostHight()
    {
        return PastHight;
    }
}

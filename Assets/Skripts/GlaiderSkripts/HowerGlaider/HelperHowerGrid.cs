using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperHowerGrid : BasicHowerGride
{

    private float HelperCushionHeight;
 


    public void initializationHowerGride(LayerMask layerMask, float HelperCushionHeight)
    {
        initializationLayerMask(layerMask);
        this.HelperCushionHeight = HelperCushionHeight;
    }


    public float helpHoverGrideActive()
    {
        if (RaycastHoverGrid() > HelperCushionHeight) return 0;
        return 1;
    }

    // Функция передачи ненешней высоты


}

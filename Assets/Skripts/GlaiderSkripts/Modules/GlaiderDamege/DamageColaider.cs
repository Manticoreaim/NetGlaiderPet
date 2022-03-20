using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColaider : MonoBehaviour
{
    public ModulHP Modul;
    [SerializeField] private bool ThisModulArmore = false;
    [SerializeField] private bool AftoGetModul = true;


    private void Start()
    {
        if(AftoGetModul) Modul = GetComponentInParent<ModulHP>();
    }
    public void DamageTTT(float damage)
    {
        Modul.DamageModuls(damage);
    }

    public bool TheArmore()
    {
        return ThisModulArmore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModulHP : ModulHPBase
{

    [SerializeField]  private Text UI;
    private bool ActiveUI = true;


    private float MaxModuleHP;

    void Start()
    {
        if (UI == null) ActiveUI = false;
        MaxModuleHP = moduleHP;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            moduleHP = MaxModuleHP;
        }

        if(ActiveUI) UI.text = moduleHP.ToString();

    }

    public string GiveHPGlaider()
    {
        return moduleHP.ToString(); 
    }
}



/* Базовый класс модуля HP
 * 
 * 
 */
public class ModulHPBase : MonoBehaviour
{
    [SerializeField] protected GlaiderHP glaiderHP;
    [SerializeField] protected float moduleHP;
    [SerializeField] protected float moduleHPGoodState;
    [SerializeField] protected float moduleHPNormalState;
    [SerializeField] protected float moduleHPBadState;
    [SerializeField] protected float moduleHPDestroye;


    [SerializeField] protected float ForceProtectionModele;
    [SerializeField] protected float ForceProtectionGlaider;

    [SerializeField] protected string NameModule;


    private void Start()
    {
        if (moduleHPGoodState == 0 && moduleHPNormalState == 0 && moduleHPBadState == 0 && moduleHPDestroye == 0)
            glaiderHP.AddModule(moduleHP, NameModule);
        else glaiderHP.AddModule(moduleHP, moduleHPGoodState, moduleHPNormalState, moduleHPBadState, moduleHPDestroye, NameModule);
    }
    virtual public void DamageModuls(float Damage)
    {
        SetDamageGlaider(Damage);
    }

    private void SetDamageGlaider(float Damage)
    {
        float DamageModel = (Damage * ForceProtectionModele);
        float DamageGlaider = (Damage * ForceProtectionGlaider);
        glaiderHP.GetDamageModele(NameModule, DamageModel, DamageGlaider);

        moduleHP = moduleHP - DamageModel;
    }


}

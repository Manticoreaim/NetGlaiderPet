
using UnityEngine;

public class BaseModule : MonoBehaviour, InterfaiseModule
{
    private CenterGlaider MeCenterGlaider;
    protected int IndexDamegeModule;
    [SerializeField] protected string NameModule;
    public virtual void SetNewIdexDamage(int IndexDamage)
    {

    }

    public string SetCenterGlaider(CenterGlaider MeCenterGlaider)
    {
        this.MeCenterGlaider = MeCenterGlaider;
        return NameModule;
    }

    protected void AddHoteGlaider (float Hote)
    {
        MeCenterGlaider.AddHoteGlaider(Hote);
    }




    private bool AddEnargyGlaider (float Enargy)
    {
        return MeCenterGlaider.AddHoteGlaider(Enargy);
    }

    protected bool GiveEnargyGlaider (float Enargy)
    {
        return (AddEnargyGlaider(-Mathf.Abs(Enargy)));
    }
    protected void GetEnargyGlaider (float Enargy)
    {
        AddEnargyGlaider(Mathf.Abs(Enargy));
    }


}


 public interface InterfaiseModule
{
    public void SetNewIdexDamage(int IndexDamage);
    public string SetCenterGlaider(CenterGlaider MeCenterGlaider);
}

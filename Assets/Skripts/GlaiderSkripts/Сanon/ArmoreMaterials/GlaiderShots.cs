using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlaiderShots : BaseArmore, ArmoreInterfase
{
    public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings, float damage)
    {


        if (collision.contacts[0].otherCollider.gameObject.GetComponent<DamageColaider>() != null)
        {
            if (collision.contacts[0].otherCollider.gameObject.GetComponent<DamageColaider>().TheArmore()) // проверка не брон€ ли это а дальше код из GlaiderShotArmore
            {
                if (Vector3.Angle(collision.contacts[0].normal.normalized, -lastVelocity.normalized) > 45f && setings.numberRicochets > 0)
                {

                    if ((damageColaider = collision.contacts[0].otherCollider.gameObject.GetComponent<DamageColaider>()) != null)
                    {
                        damageColaider.DamageTTT(damage);
                    }

                    Debug.Log("косой по броне");
                    ReflectBolle(collision, lastVelocity, RB, ref setings);

                    bullet.SetLaunchVelocity(lastVelocity);
                }
                else
                {
                    RaycastHit[] HitModels;
                    HitModels = Physics.RaycastAll(RB.transform.position, lastVelocity, setings.ForcePenetration);

                    Debug.DrawRay(collision.contacts[0].point, lastVelocity.normalized * setings.ForcePenetration, Color.red, 4f);

                    Debug.Log(" ”дар по броне");
                    for (int i = 0; HitModels.Length > i; i++)
                    {
                        if ((damageColaider = HitModels[i].collider.gameObject.GetComponent<DamageColaider>()) != null)
                        {
                            damageColaider.DamageTTT(damage);
                        }
                    }
                    bullet.DestroyBullet();
                }

                bullet.InstantiateSparks(collision.contacts[0].normal, collision.contacts[0].point);
            }
            else
            {
                if ((damageColaider = collision.contacts[0].otherCollider.gameObject.GetComponent<DamageColaider>()) != null)
                {
                    damageColaider.DamageTTT(damage);
                }

                RaycastHit[] HitModels;
                HitModels = Physics.RaycastAll(RB.transform.position, lastVelocity, setings.ForcePenetration);

                Debug.DrawRay(collision.contacts[0].point, lastVelocity.normalized * setings.ForcePenetration, Color.red, 4f);

                Debug.Log(" ”дар по корпусу");
                Debug.Log(collision.gameObject.layer);
                for (int i = 0; HitModels.Length > i; i++)
                {
                    if ((damageColaider = HitModels[i].collider.gameObject.GetComponent<DamageColaider>()) != null)
                    {
                        damageColaider.DamageTTT(damage);
                    }
                }


                bullet.InstantiateSparks(collision.contacts[0].normal, collision.contacts[0].point);
                bullet.DestroyBullet();
            }
        }
        else
        {
            bullet.DestroyBullet();
        }
    }
}

/*
  * 
  public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings, float damage)
    {
        if (collision.gameObject.GetComponent<DamageColaider>().TheArmore()) // проверка не брон€ ли это а дальше код из GlaiderShotArmore
        {
            if (Vector3.Angle(collision.contacts[0].normal.normalized, -lastVelocity.normalized) > 45f && setings.numberRicochets > 0)
            {

                if ((damageColaider = collision.gameObject.GetComponent<DamageColaider>()) != null)
                {
                    damageColaider.DamageTTT(damage);
                }

                Debug.Log("косой по броне");
                ReflectBolle(collision, lastVelocity, RB, ref setings);

                bullet.SetLaunchVelocity(lastVelocity);
            }
            else
            {
                RaycastHit[] HitModels;
                HitModels = Physics.RaycastAll(RB.transform.position, lastVelocity, setings.ForcePenetration);

                Debug.DrawRay(collision.contacts[0].point, lastVelocity.normalized * setings.ForcePenetration, Color.red, 4f);

                Debug.Log(" ”дар по броне");
                for (int i = 0; HitModels.Length > i; i++)
                {
                    if ((damageColaider = HitModels[i].collider.gameObject.GetComponent<DamageColaider>()) != null)
                    {
                        damageColaider.DamageTTT(damage);
                    }
                }
                bullet.DestroyBullet();
            }

            bullet.InstantiateSparks(collision.contacts[0].normal);
        }
        else
        {

            RaycastHit[] HitModels;
            HitModels = Physics.RaycastAll(RB.transform.position, lastVelocity, setings.ForcePenetration);

            Debug.DrawRay(collision.contacts[0].point, lastVelocity.normalized * setings.ForcePenetration, Color.red, 4f);

            Debug.Log(" ”дар по броне");
            Debug.Log(collision.gameObject.layer);
            for (int i = 0; HitModels.Length > i; i++)
            {
                if ((damageColaider = HitModels[i].collider.gameObject.GetComponent<DamageColaider>()) != null)
                {
                    damageColaider.DamageTTT(damage);
                }
            }


            bullet.InstantiateSparks(collision.contacts[0].normal);
            bullet.DestroyBullet();
        }
    }
  * 
  * 
  */

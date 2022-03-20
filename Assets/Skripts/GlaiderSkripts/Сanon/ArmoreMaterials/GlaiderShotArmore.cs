using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlaiderShotArmore : BaseArmore, ArmoreInterfase
{
    public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings, float damage)
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
            HitModels = Physics.RaycastAll(RB.transform.position, lastVelocity , setings.ForcePenetration);

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
}

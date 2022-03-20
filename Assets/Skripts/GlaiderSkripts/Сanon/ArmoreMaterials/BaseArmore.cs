using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArmore: MonoBehaviour
{

    private int testLyerMask = 191; //маска физслоёв( все кроме пули)

    protected DamageColaider damageColaider;
    protected void ReflectBolle(Collision collision, Vector3 lastVelocity, Rigidbody RB, ref SetingsBullet setings)
    {
        ContactPoint cp = collision.contacts[0];
        lastVelocity = Vector3.Reflect(lastVelocity, cp.normal);
        RB.velocity = lastVelocity;
        RB.transform.rotation.SetLookRotation(lastVelocity);
        setings.numberRicochets = setings.numberRicochets - 1;
    }

    protected void PenetrationBullet(Rigidbody RB, Vector3 lastVelocity , Bolle bullet , float MaxDistansPenetration, ref SetingsBullet setings)
    {

        float OffsetRayPenetration = MaxDistansPenetration - 0.1f;

        RaycastHit Hit;

        Debug.DrawRay(RB.transform.position + lastVelocity.normalized * MaxDistansPenetration, -lastVelocity.normalized * OffsetRayPenetration, Color.black, 2);

        if (Physics.Raycast(RB.transform.position + lastVelocity.normalized * MaxDistansPenetration, -lastVelocity.normalized, out Hit, OffsetRayPenetration, testLyerMask))
        {
            Debug.Log("пробитие");
            // RB.transform.position = Hit.point + lastVelocity.normalized * RB.transform.localScale.x;
            bullet.ColliderSwithe();
            RB.velocity = lastVelocity;

            setings.ForcePenetration = setings.ForcePenetration - Hit.distance;

            bullet.SetSetingsBullt(setings);

            bullet.SetLaunchVelocity(lastVelocity);


        }
        else
        {
            Debug.Log("нет пробитие");
            bullet.DestroyBullet();
        }
    }

}

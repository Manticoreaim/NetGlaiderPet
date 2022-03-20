using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ArmoreInterfase
{
    public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings , float damage);

}

public class ArmoreMetall : BaseArmore, ArmoreInterfase
{


    public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings, float damage)
    {
        Debug.Log("металл");
        if (Vector3.Angle(collision.contacts[0].normal.normalized, -lastVelocity.normalized) > 45f && setings.numberRicochets > 0)
        {
            Debug.Log("рекошет");

            ReflectBolle(collision, lastVelocity, RB, ref setings);

            bullet.SetLaunchVelocity(lastVelocity);
        }
        else
        {
            Debug.Log("попытка пробития:");
            PenetrationBullet(RB, lastVelocity, bullet, setings.ForcePenetration, ref setings);
            
        }

        bullet.InstantiateSparks(collision.contacts[0].normal , collision.contacts[0].point);

    }


}

public class ArmoreSoft : ArmoreInterfase
{
    // Start is called before the first frame update
    public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings, float damage)
    {
        Debug.Log(collision.gameObject.tag);


        float MaxDistansPenetration = 1;
        float OffsetRayPenetration = MaxDistansPenetration - 0.1f;

        RaycastHit Hit;
        Debug.DrawRay(RB.transform.position + lastVelocity.normalized * MaxDistansPenetration, -lastVelocity.normalized * OffsetRayPenetration, Color.black, 10);
        if (Physics.Raycast(RB.transform.position + lastVelocity.normalized * MaxDistansPenetration, -lastVelocity.normalized, out Hit, OffsetRayPenetration))
        {

            RB.transform.position = Hit.point + lastVelocity.normalized;
            RB.velocity = lastVelocity;

            bullet.SetLaunchVelocity(lastVelocity);

        }
        else
        {
            bullet.DestroyBullet();
        }
    }
}


public class ArmoreSolid : ArmoreInterfase
{
    // Start is called before the first frame update
    public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings, float damage)
    {
        Debug.Log(collision.gameObject.tag);

        ContactPoint cp = collision.contacts[0];
        lastVelocity = Vector3.Reflect(lastVelocity, cp.normal);
        RB.velocity = lastVelocity;


        bullet.SetLaunchVelocity(lastVelocity);

        bullet.InstantiateSparks(collision.contacts[0].normal, collision.contacts[0].point);
    }
}

public class ArmoreBullet : ArmoreInterfase
{
    // Start is called before the first frame update
    public void GetProjectilebullet(Collision collision, Vector3 lastVelocity, Rigidbody RB, Bolle bullet, ref SetingsBullet setings, float damage)
    {
        bullet.InstantiateSparks(collision.contacts[0].normal, collision.contacts[0].point);
        bullet.ColliderSwithe();
    }
}
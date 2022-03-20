using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetProjectileType : MonoBehaviour
{
   public void SetProjectile(GameObject projectileType)
   {
        GameObject gameObject = Instantiate(projectileType, this.transform);
        gameObject.transform.SetParent(this.transform);
    }
}

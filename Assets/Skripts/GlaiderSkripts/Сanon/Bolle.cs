using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using Photon.Pun;

public class Bolle : MonoBehaviour
{
    [SerializeField] private Rigidbody RigidbodyBolle;
    [SerializeField] private Collider ColliderBolle;

    [SerializeField] private float launchVelocity;

    // [SerializeField] private GameObject ParticalHitMetall;
    [SerializeField] private GameObject ParticalHitMetallLocal;

    //[SerializeField] private GameObject SaundHitMetall;
   // [SerializeField] private AudioSource SaundJHitMetall;

    [SerializeField] private float lifeTimeBolle;






    [SerializeField] private float ForcePenetration;
    [SerializeField] private float numberRicochets;
    [SerializeField] private float DamageBullet;

    private Vector3 lastVelocity;

    private List<string> keylist = new List<string> { "Metal", "Solid", "Soft", "Glaider", "GlaiderArmore", "Bullet" };



    public SetingsBullet Setings;
    private Dictionary<string, ArmoreInterfase> Projectilebullet;

    [SerializeField] private float coolDwon = 0.1f;

    private void Awake() // кастыль на котором все держиться
    {
        ColliderSwithe();
    }

    void Start()
    {
        RigidbodyBolle.AddRelativeForce(new Vector3(0, launchVelocity, 0));
        Initbehavers();

        Setings.Speed = launchVelocity;
        Setings.ForcePenetration = ForcePenetration;
        Setings.numberRicochets = numberRicochets;
        Setings.DamageBulle = DamageBullet;

        StartCoroutine("DestroyBolle");


    }
    void FixedUpdate()
    {
        lastVelocity = RigidbodyBolle.velocity;
        RigidbodyBolle.velocity = lastVelocity;
    }

    private void Initbehavers()
    {
        this.Projectilebullet = new Dictionary<string, ArmoreInterfase>();

        this.Projectilebullet["Metal"] = new ArmoreMetall();
        this.Projectilebullet["Solid"] = new ArmoreSolid();
        this.Projectilebullet["Soft"] = new ArmoreSoft();
        this.Projectilebullet["Glaider"] = new GlaiderShots();
        this.Projectilebullet["GlaiderArmore"] = new GlaiderShotArmore();
        this.Projectilebullet["Bullet"] = new ArmoreBullet();

       // this.keylist = new List<string> { "Metal", "Solid" , "Soft", "Glaider", "GlaiderArmore" };
    }


    private void OnCollisionEnter(Collision collision)
    {
        

        string hitMaterial = collision.gameObject.tag;
        ////Debug.Log(hitMaterial);
        //Collision CollisionShot;
        //if (collision.gameObject.tag != "GlaiderMe")
        //{
        //    CollisionShot = collision;
        //}
        //else
        //{
        //    return;
        //}
        //Debug.Log(CollisionShot.gameObject.name);


        if (this.keylist.Contains(hitMaterial) == true && collision != null)
        {
            this.Projectilebullet[hitMaterial].GetProjectilebullet(collision, lastVelocity, RigidbodyBolle, this, ref Setings, DamageBullet);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    public void SetLaunchVelocity(Vector3 lastVelocity)
    {
        this.lastVelocity = lastVelocity;
    }

    public void SetSetingsBullt(SetingsBullet setings)
    {
        this.Setings = setings;
    }

    public void ColliderSwithe()
    {
        StartCoroutine("bulletPenetration");
    }

    public void InstantiateSparks(Vector3 normal , Vector3 postion)
    {
        //PhotonNetwork.Instantiate(ParticalHitMetall.name, transform.position, Quaternion.LookRotation(normal, Vector3.up));
        //PhotonNetwork.Instantiate(SaundHitMetall.name, transform.position, Quaternion.LookRotation(normal, Vector3.up));

        Instantiate(ParticalHitMetallLocal, postion, Quaternion.LookRotation(normal, Vector3.up));

        //SaundJHitMetall.pitch = Random.Range(0.7f, 1.2f);
       // SaundJHitMetall.Play();
     //   Instantiate(ParticalHitMetall, transform.position, Quaternion.LookRotation(normal, Vector3.up));
     //   Instantiate(SaundHitMetall, transform.position, Quaternion.LookRotation(normal, Vector3.up));
    }





    private IEnumerator DestroyBolle()
    {
        yield return new WaitForSeconds(lifeTimeBolle);
        Destroy(this.gameObject);
    }

    private IEnumerator bulletPenetration()
    {
        ColliderBolle.enabled = !ColliderBolle.enabled;
        yield return new WaitForSeconds(coolDwon);
        ColliderBolle.enabled = !ColliderBolle.enabled;
    }


}
public struct SetingsBullet
{
    public float Speed { get; set; }
    public float ForcePenetration { get; set; }
    public float numberRicochets { get; set; }

    public float DamageBulle { get; set; }



}



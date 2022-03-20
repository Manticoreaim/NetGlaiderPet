using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControllerCannon : MonoBehaviour
{
    [SerializeField] private GameObject projectileNet;
    [SerializeField] private Rigidbody RBGlaider;


    [SerializeField] private Transform Barrel;
    [SerializeField] private AudioSource SoundShot;

    [SerializeField] private float CooldownTimeShot = 0.5f;

    private bool ActiveShot = true;

    private void Start()
    {
        RBGlaider = GetComponentInParent<Rigidbody>();
    }
    public void GunShot()
    {
        if (ActiveShot) // проверка выстрела
        {
           GameObject Bulle = PhotonNetwork.Instantiate(projectileNet.name, Barrel.position, Barrel.rotation);
            Bulle.GetComponent<Rigidbody>().velocity = RBGlaider.velocity;
            //PhotonNetwork.Instantiate(GubShotSound.name, Barrel.position, Barrel.rotation);

            SoundShot.pitch = Random.Range(0.95f, 1.05f);
            SoundShot.PlayOneShot(SoundShot.clip);

           // Instantiate(projectile, Barrel.position, Barrel.rotation); // cоздание снаряда
           // Instantiate(GubShotSound, Barrel.position, Barrel.rotation); // созданиен звука выстрела

            StartCoroutine("CooldownShotTaimer"); // задержка перед выстрелом
        }
    }


    private IEnumerator CooldownShotTaimer()
    {
        ActiveShot = false;
        yield return new WaitForSeconds(CooldownTimeShot);
        ActiveShot = true;
    }
}

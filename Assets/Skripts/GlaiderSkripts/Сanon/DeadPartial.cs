using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPartial : MonoBehaviour
{
    public GameObject Partical;
    public GameObject Lith;

    public AudioSource SaundHitMetall;

    public float LifeTime;
    void Start()
    {
        StartCoroutine("DestroySprait");
        SaundHitMetall.pitch = Random.Range(0.7f, 1.2f);
        SaundHitMetall.Play();
    }


    private IEnumerator DestroySprait()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(Partical);
    }
}

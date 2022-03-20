using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePartical : MonoBehaviour
{
    [SerializeField] private float CooldownTimeShot;
    [SerializeField] private AudioSource  SoundHitMetall;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DeadTimerGameObject"); // задержка перед выстрелом
        SoundHitMetall.pitch = Random.Range(0.6f, 1.3f);
        SoundHitMetall.timeSamples = 1;
        SoundHitMetall.Play();
        
    }

    // Update is called once per frame
    private IEnumerator DeadTimerGameObject()
    {
        yield return new WaitForSeconds(CooldownTimeShot);
        Destroy(this.gameObject);
    }
}

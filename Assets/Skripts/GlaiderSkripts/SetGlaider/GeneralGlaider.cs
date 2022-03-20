using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGlaider : MonoBehaviour
{
    [SerializeField] private GameObject Glaider;
    [SerializeField] private GameObject LeftGun;
    [SerializeField] private GameObject RightGun;


    [SerializeField] private Transform LeftGunSetupTransform;
    [SerializeField] private Transform RightGunSetupTransform;

    private void Awake()
    {
        GameObject gameObject = Instantiate(Glaider, this.transform);
        gameObject.transform.SetParent(this.transform);



        gameObject = Instantiate(LeftGun, LeftGunSetupTransform);
        gameObject.transform.SetParent(LeftGunSetupTransform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

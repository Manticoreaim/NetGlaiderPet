
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] SpawnPoint;

    public GameObject Glaider;
    public GameObject Pleyer;
    public GameObject Convas;

    private void Awake()
    {
        Vector3 RandomSpawn = SpawnPoint[Random.Range(0, SpawnPoint.Length)].transform.localPosition;

        PhotonNetwork.Instantiate(Glaider.name, RandomSpawn, Quaternion.identity);
        //Instantiate(Glaider, RandomSpawn);
     //   PhotonNetwork.Instantiate(Pleyer.name, RandomSpawn + new Vector3(0,4,-7), Quaternion.identity);
     //   PhotonNetwork.InstantiateRoomObject(Convas.name, Vector3.zero, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Public Variables 
#region
    public GameObject Cross;
    public GameObject Sphere;
    public GameObject Bolt;


    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public Transform spawnPoint6;
    public Transform spawnPoint7;
    public Transform spawnPoint8;
    public Transform spawnPoint9;

    public GameObject p1, p2, p3, p4, p5, p6, p7, p8, p9;

    


    #endregion
    public float timeGap = 1.0f;


    private Transform[] spawnPoints;
    private GameObject[] pieces;
    private GameObject[] platforms;
    private float nextPlay = 0.0f;


    void Start()
    {
        spawnPoints = new Transform[]{spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4, spawnPoint5, spawnPoint6, spawnPoint7, spawnPoint8, spawnPoint9};
        pieces = new GameObject[] { Cross, Sphere, Bolt };
        platforms = new GameObject[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 };
    }

    void SpawnPiece(int playerID, int sp) {
        GameObject object_to_spawn = pieces[playerID-1];
        Transform spawnPoint = spawnPoints[sp-1];
        Instantiate(object_to_spawn, spawnPoint.position, spawnPoint.rotation);
        return;
    }


    public void SpawnSequence(int playerID, int move)
    {
        SpawnPiece(playerID, move);
        nextPlay = Time.time + timeGap;
    }

    public void DeSpawnSequence(int move) {
        platforms[move - 1].GetComponent<PlatformManager>().Reset();
    }

    public bool CanPlay(bool playing =true) {
        //Debug.Log("The time is " + Time.time + " and nextPlay is " + nextPlay);
        if (!playing) {
        return (Time.time >= nextPlay+1.5*timeGap);
        }
        return (Time.time >= nextPlay);
    }

    // Update is called once per frame

    void Update()
    {

        
    }
}

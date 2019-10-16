using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] checkPoints;
    public Transform field;
    public float timeGap = 10.0f;
    public float rotateSpeed = 1;
    private NavMeshAgent ai;
    private float nextSwitch = 0.0f;


    void Start()
    {
        ai = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSwitch) {
            nextSwitch = Time.time + timeGap;
            int randomIndex = (( int )(Random.value*100))%checkPoints.Length;
            ai.SetDestination(checkPoints[randomIndex].position);
            //Debug.Log(Time.time+" with nextSwitch " + nextSwitch);
        }
        Vector3 targetDir = field.position - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateSpeed*Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

    }
}

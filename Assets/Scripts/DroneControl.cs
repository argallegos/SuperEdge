using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour {

    public Transform pos1, pos2;

    Vector3 direction;
    Transform destination;
    public GameObject drone;
    Rigidbody droneRB;
    public float moveSpeed;

    public float waitTime;
    private bool move = false;
    private float waitCounter = 0f;

    void Start () {
        SetDestination(pos1);
        droneRB = drone.GetComponent<Rigidbody>();
    }
	
	void FixedUpdate () {

        if (move)
        {
            droneRB.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);

            if ((Vector3.Distance(transform.position, destination.position) < moveSpeed * Time.fixedDeltaTime)) 
            {
                SetDestination(destination == pos1 ? pos2 : pos1);
                if (move) Stopped();
            }
        }
        else
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                EndWait();
            }
        }
    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        Vector3 targetPostition = new Vector3(dest.position.x, this.transform.position.y, dest.position.z);
        this.transform.LookAt(targetPostition);
        direction = (destination.position - transform.position).normalized;
    }

    void EndWait()
    {
        waitCounter = 0f;
        move = true;
    }

    void Stopped()
    {
        move = false;
    }
}

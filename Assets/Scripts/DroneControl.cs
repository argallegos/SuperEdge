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
/*
// © 2017 TheFlyingKeyboard and released under MIT License
// theflyingkeyboard.net
//Moves object between two points
public class MoveBetweenTwoPoints : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private bool reverseMove = false;
    [SerializeField] private Transform objectToUse;
    [SerializeField] private bool moveThisObject = false;
    private float startTime;
    private float journeyLength;
    private float distCovered;
    private float fracJourney;
    void Start()
    {
        startTime = Time.time;
        if (moveThisObject)
        {
            objectToUse = transform;
        }
        journeyLength = Vector3.Distance(pointA.transform.position, pointB.transform.position);
    }
    void Update()
    {
        distCovered = (Time.time - startTime) * moveSpeed;
        fracJourney = distCovered / journeyLength;
        if (reverseMove)
        {
            objectToUse.position = Vector3.Lerp(pointB.transform.position, pointA.transform.position, fracJourney);
        }
        else
        {
            objectToUse.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, fracJourney);
        }
        if ((Vector3.Distance(objectToUse.position, pointB.transform.position) == 0.0f || Vector3.Distance(objectToUse.position, pointA.transform.position) == 0.0f)) //Checks if the object has travelled to one of the points
        {
            if (reverseMove)
            {
                reverseMove = false;
            }
            else
            {
                reverseMove = true;
            }
            startTime = Time.time;
        }
    }
}
public Transform pos1, pos2;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool reverseMove = false;
    [SerializeField] private Transform objectToUse;
    [SerializeField] private bool moveThisObject = false;
    private float startTime;
    private float journeyLength;
    private float distCovered;
    private float fracJourney;

    public float waitTime;
    private bool move = false;
    private float waitCounter = 0f;

    void Start () {
        startTime = Time.time;
        if (moveThisObject)
        {
            objectToUse = transform;
        }
        journeyLength = Vector3.Distance(pos1.position, pos2.position);
    }
	
	void FixedUpdate () {

        if (move)
        {
            distCovered = (Time.time - startTime) * moveSpeed;
            fracJourney = distCovered / journeyLength;

            if (reverseMove)
            {
                objectToUse.position = Vector3.Lerp(pos2.position, pos1.position, fracJourney);
            }
            else
            {
                objectToUse.position = Vector3.Lerp(pos1.position, pos2.position, fracJourney);
            }

            if ((Vector3.Distance(objectToUse.position, pos2.position) == 0.0f || Vector3.Distance(objectToUse.position, pos1.position) == 0.0f))
            {
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
    void EndWait()
    {
        waitCounter = 0f;
        if (reverseMove)
        {
            reverseMove = false;
        }
        else
        {
            reverseMove = true;
        }
        move = true;
        startTime = Time.time;
    }

    void Stopped()
    {
        move = false;


    }
*/


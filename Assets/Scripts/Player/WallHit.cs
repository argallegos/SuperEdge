using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour {

    public GameObject player;
    WallClimb climbScript;

    void Start()
    {
        climbScript = player.GetComponent<WallClimb>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {

            climbScript.Climb();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            print("not climbing");
            climbScript.StopClimb();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public GameObject player;
    GripEdge gripScript;
    WallClimb climbScript;

    void Start()
    {
        gripScript = player.GetComponent<GripEdge>();
        climbScript = player.GetComponent<WallClimb>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            print("Grabbing!");
            gripScript.Hang();
            gripScript.hanging = true;
            gripScript.hangPos.Set(player.transform.position.x, other.transform.position.y, player.transform.position.z);
        }
        else if (other.CompareTag("Wall"))
        {
            climbScript.Climb();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            gripScript.StopHang();
        }
        else if (other.CompareTag("Wall"))
        {
            print("not climbing");
            climbScript.StopClimb();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public GameObject player;
    GripEdge gripScript;
    WallClimb climbScript;
    PlayerScript playerScript;

    void Start()
    {
        gripScript = player.GetComponent<GripEdge>();
        climbScript = player.GetComponent<WallClimb>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            print("Grabbing!");
            gripScript.hangPos = new Vector3(player.transform.position.x, other.transform.position.y, player.transform.position.z);
            gripScript.Hang();
            gripScript.hanging = true;

        }
        else if (other.CompareTag("Wall"))
        {
            climbScript.Climb();
        }
        if (other.CompareTag("WinCube"))
        {
            playerScript.win = true;
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

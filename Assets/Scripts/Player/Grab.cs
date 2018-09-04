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
            playerScript.AnimState("hang");

        }
        else if (other.CompareTag("DroneEdge"))
        {
            print("Grabbing Drone!");
            gripScript.hangPos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            gripScript.Hang();
            gripScript.hanging = true;
            gripScript.droneHang = true;
            playerScript.AnimState("hang");
        }
        else if (other.CompareTag("Wall"))
        {
            climbScript.Climb();
        }
        else if (other.CompareTag("AddSpeed"))
        {
            // playerScript.speed = playerScript.speedySpeed;
           // playerScript.Launch();
        }

        else if (other.CompareTag("WinCube"))
        {
            playerScript.win = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AddSpeed"))
        {
            playerScript.launchDirection = other.transform.forward;
            playerScript.Launch();
        }
        else if (other.CompareTag("DroneEdge"))
        {
            gripScript.hangPos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Edge") || other.CompareTag("DroneEdge"))
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

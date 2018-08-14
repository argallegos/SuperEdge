using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public GameObject player;
    GripEdge gripScript;

    void Start()
    {
        gripScript = player.GetComponent<GripEdge>();
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
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            gripScript.StopHang();
        }
    }

}

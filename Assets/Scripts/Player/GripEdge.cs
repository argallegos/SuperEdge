using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripEdge : MonoBehaviour {

    InputController playerInput;
    PlayerScript playerScript;

    [HideInInspector]
    public bool hanging, droneHang;

    public float jumpUpForce, hangHeight, hangOffset;

    [HideInInspector]
    public Vector3 hangPos;

    void Start () {
        playerScript = GetComponent<PlayerScript>();
        hanging = false;
	}
	
	void Update () {
		if (hanging)
        {
            if (droneHang)
            {
                playerScript.transform.position = new Vector3(hangPos.x, hangPos.y - hangOffset, hangPos.z);
            }
            if (playerScript.playerInput.jump)
            {
                StopHang();
                playerScript.playerRB.AddForce(Vector3.up * jumpUpForce);
                playerScript.AnimState("jump");
            }
            if (playerScript.playerInput.shift)
            {
                StopHang();
                playerScript.AnimState("falling");
            }
        }
	}

    public void Hang ()
    {
        playerScript.transform.position = new Vector3(hangPos.x, hangPos.y-hangOffset, hangPos.z);
        playerScript.playerRB.velocity = Vector3.zero;
        playerScript.playerRB.useGravity = false;

        hanging = true;
        playerScript.hanging = true;

    }

    public void StopHang()
    {
        hanging = false;
        playerScript.playerRB.useGravity = true;
        playerScript.hanging = false;
    }

}

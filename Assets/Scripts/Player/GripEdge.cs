using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripEdge : MonoBehaviour {

    InputController playerInput;
    PlayerScript playerScript;

    [HideInInspector]
    public bool hanging;

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
            if (playerScript.playerInput.jump)
            {
                StopHang();
                playerScript.playerRB.AddForce(Vector3.up * jumpUpForce);
            }
            if (playerScript.playerInput.shift)
            {
                StopHang();
            }

        }

	}

    public void Hang ()
    {
        playerScript.transform.position = new Vector3(hangPos.x, hangPos.y-hangOffset, hangPos.z);
        // playerScript.playerRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        playerScript.playerRB.velocity = Vector3.zero;
        playerScript.playerRB.useGravity = false;
        //playerScript.meshSwitch.SwitchMesh(playerScript.meshSwitch.wallHang);
        Debug.Log(hangPos + " " + hangOffset);

        hanging = true;
        print("hanging");
        print(hangPos);
    }

    public void StopHang()
    {
        //playerScript.playerRB.constraints = RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationZ;
        hanging = false;
        playerScript.playerRB.useGravity = true;
        print("not hanging");
        //playerScript.meshSwitch.SwitchMesh(playerScript.meshSwitch.wallJump);
    }


    public void Lift()
    {

    }

    public void Drop ()
    {

    }


}

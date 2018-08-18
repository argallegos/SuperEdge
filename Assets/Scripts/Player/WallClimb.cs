using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : MonoBehaviour {

    InputController playerInput;
    PlayerScript playerScript;

    [HideInInspector]
    public bool climbing;


    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        climbing = false;
    }
	

	void Update () {
        if (climbing && playerScript.playerInput.Vertical > 0f)
        {
            playerScript.playerRB.AddForce(Vector3.up * 10);
        }
	}

    public void Climb()
    {
        if (playerScript.playerRB.velocity.y > 1f && playerScript.playerInput.Vertical > 0f)
        {
            climbing = true;
            print("climbing");
        }
    }

    public void StopClimb()
    {
        climbing = false;
    }
}

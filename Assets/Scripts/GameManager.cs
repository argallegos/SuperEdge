using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player;

    public Camera playerCam;

    Transform respawnPoint;

    public float fallDeathPoint;

	void Start () {
        respawnPoint = player.transform;
	}
	
	void Update () {

        if (player.transform.position.y < fallDeathPoint) Respawn();    
		
	}

    void Respawn()
    {
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player, spawnPoint;

    public Camera playerCam;

    Transform respawnPoint;

    public float fallDeathPoint;

	void Start () {
        respawnPoint = spawnPoint.transform;
	}
	
	void Update () {

        if (player.transform.position.y < fallDeathPoint) Respawn();    
		
	}

    void Respawn()
    {
        print("Respawn!");
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
    }


}

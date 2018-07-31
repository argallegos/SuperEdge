using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public GameObject player;

    Vector3 respawnPoint;

    public float fallDeathPoint;

	void Start () {
        respawnPoint = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (player.transform.position.y < fallDeathPoint)
            player.transform.position = respawnPoint;
		
	}
}

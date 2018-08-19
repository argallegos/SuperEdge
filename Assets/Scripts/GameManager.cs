using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player, spawnPoint;

    public Camera playerCam;
    ThirdPersonCamera cam;

    Transform respawnPoint;
    public Transform debugSpawn;
    public bool altSpawn;


    public float fallDeathPoint;

    public float restartDelay = 1f;
    private bool restartReady = false;
    private float restartCounter = 0f;

    void Start () {
        SetSpawn();
        cam = playerCam.GetComponent<ThirdPersonCamera>();
    }
	
	void Update () {

        if (player.transform.position.y < fallDeathPoint)
        {
            Dying();
            if (restartReady)
            {
                Respawn();
                //if (Input.anyKeyDown) Respawn();
            }
            else
            {
                restartCounter += Time.deltaTime;
                if (restartCounter >= restartDelay)
                {
                    restartReady = true;
                }
            }
        }

    }

    void SetSpawn()
    {
        if (altSpawn) respawnPoint = debugSpawn;

        else respawnPoint = spawnPoint.transform;
    }

    void Dying()
    {
        cam.cameraMove = false;
        //Respawn();
        
    }



    void Respawn()
    {
        print("Respawn!");
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
        cam.CamReset();
        cam.cameraMove = true;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player, spawnPoint;

    public Camera playerCam;
    ThirdPersonCamera cam;

    Transform respawnPoint;

    public float fallDeathPoint;

    public float restartDelay = 1f;
    private bool alive = true;
    private bool restartReady = false;
    private float restartCounter = 0f;

    void Start () {
        respawnPoint = spawnPoint.transform;
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

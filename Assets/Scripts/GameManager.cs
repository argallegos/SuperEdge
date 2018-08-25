using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject player, spawnPoint;

    public Camera playerCam;
    ThirdPersonCamera cam;
    PlayerScript playerScript;

    Transform respawnPoint;
    public Transform debugSpawn;
    public bool altSpawn;


    public float fallDeathPoint;

    public float restartDelay = 1f;
    private bool restartReady = false;
    private float restartCounter = 0f;
    public Text winText;

    void Start () {
        SetSpawn();
        cam = playerCam.GetComponent<ThirdPersonCamera>();
        playerScript = player.GetComponent<PlayerScript>();

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
        if (playerScript.win) winText.text = "YOU WIN!!!!!!!!!!!!!!!!!!!!!!!!! ";

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCube"))
        {
            winText.text = "YOU WIN!!!!!!!!!!!!!!!!!!!!!!!!! ";
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

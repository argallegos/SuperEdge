using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject player, UI, spawnPoint;

    public Camera playerCam;
    ThirdPersonCamera cam;
    PlayerScript playerScript;
    GamePlay_UI_Script UIScript;

    Transform respawnPoint;
    public Transform debugSpawn1, debugSpawn2, debugSpawn3, debugSpawn4, debugSpawn5;

    public float fallDeathPoint;
    public float restartDelay = 1f;
    private bool restartReady = false;
    private float restartCounter = 0f;
    public bool paused = false;

    AudioSource source;
    public AudioClip music;
    public AudioClip scream;
    public float volume;
    bool isScreaming = false;

    void Start () {
        respawnPoint = spawnPoint.transform;
        cam = playerCam.GetComponent<ThirdPersonCamera>();
        playerScript = player.GetComponent<PlayerScript>();
        UIScript = UI.GetComponent<GamePlay_UI_Script>();

        source = GetComponent<AudioSource>();
        source.Play();
		Cursor.lockState = CursorLockMode.Locked;

    }
	
	void Update () {

        if (player.transform.position.y < fallDeathPoint)
        {
            Dying();
            if (restartReady)
            {
                Respawn();

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

        if (playerScript.win) Win();

        if (paused && !playerScript.paused) playerScript.paused = true;
        else if (!paused && playerScript.paused) playerScript.paused = false;

        if (playerScript.inAir) UIScript.AddScore(0.1f);

        if (Input.GetKeyDown(KeyCode.Alpha1)) SetSpawn(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetSpawn(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetSpawn(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetSpawn(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetSpawn(5);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SetSpawn(0);
    }

    void SetSpawn(int number)
    {
        if (number == 1)
        {
            respawnPoint = debugSpawn1;
            Respawn();
        }
        if (number == 2)
        {
            respawnPoint = debugSpawn2;
            Respawn();
        }
        if (number == 3)
        {
            respawnPoint = debugSpawn3;
            Respawn();
        }
        if (number == 4)
        {
            respawnPoint = debugSpawn4;
            Respawn();
        }
        if (number == 5)
        {
            respawnPoint = debugSpawn5;
            Respawn();
        }
        if (number == 0)
        {
            respawnPoint = spawnPoint.transform;
            Respawn();
        }
    }

    void Dying()
    {
        cam.cameraMove = false;

        if (!isScreaming)
        {
            source.PlayOneShot(scream, volume);
            isScreaming = true;
        }
    }

    void Respawn()
    {
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
        cam.CamReset();
        cam.cameraMove = true;
        isScreaming = false;
    }
    public void Win()
    {
        UIScript.ShowWinScreen();
    }

}

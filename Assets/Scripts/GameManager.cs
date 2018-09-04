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
    public Transform debugSpawn;
    public bool altSpawn;


    public float fallDeathPoint;

    public float restartDelay = 1f;
    private bool restartReady = false;
    private float restartCounter = 0f;
    public Text winText;
    public bool paused = false;

    AudioSource source;
    bool m_Play;
    bool m_ToggleChange;
    public AudioClip music;
    public AudioClip scream;
    public float volume;
    bool isScreaming = false;

    void Start () {
        SetSpawn();
        cam = playerCam.GetComponent<ThirdPersonCamera>();
        playerScript = player.GetComponent<PlayerScript>();
        UIScript = UI.GetComponent<GamePlay_UI_Script>();

        source = GetComponent<AudioSource>();
        source.Play();
        //source.clip = scream;
        m_Play = true;

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

        if (playerScript.inAir) {
            UIScript.AddScore(0.1f);
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

        if (!isScreaming)
        {
            source.PlayOneShot(scream, volume);
            isScreaming = true;
        }
        //Respawn();

    }

    void Respawn()
    {
        print("Respawn!");
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
        cam.CamReset();
        cam.cameraMove = true;
        isScreaming = false;
    }
    public void Win()
    {
        //winText.text = "YOU WIN!!!!!!!!!!!!!!!!!!!!!!!!! ";
        UIScript.ShowWinScreen();

    }


}

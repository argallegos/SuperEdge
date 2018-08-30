using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlay_UI_Script : MonoBehaviour {

protected static GamePlay_UI_Script _instance = null;
	
	public static GamePlay_UI_Script Instance = null;

#region Variables
	public float SecondsPassed;
	public int MinutesPassed;
	public Text TimerText;
	
	public GameObject PauseUI;

    public GameObject gameManager;
    GameManager manager;
	
	public int CurrentScore;
	public Text ScoreText;
	
	public float fadeTime, percentFade;
	public GameObject winScreen;
	public Text EndTimerText, EndScoreText;
	public bool winner;
#endregion
#region General Functions
	
	void Start(){
		if (Instance == null){
			Instance = this;
		} else if (Instance != this){
			Destroy(this.gameObject);
		}

        manager = gameManager.GetComponent<GameManager>();

        SecondsPassed = 0.0f;
		MinutesPassed = 0;
		
		CurrentScore = 0;
		ScoreText.text = "Score: " + CurrentScore;
		
		winScreen.GetComponent<CanvasGroup>().alpha = 0f;
	}
	
	void Update(){
		UpdateTimeText();
		
		if(Input.GetKeyDown(KeyCode.P)){
			PauseGame();
		}
		
		if(Input.GetKeyDown(KeyCode.B)){
			ShowWinScreen();
		}
		
		if(Input.GetKeyDown(KeyCode.M)){
			AddScore(10);
		}
	}
	
	public void UpdateTimeText(){
		if(!winner){
			if(SecondsPassed >= 60){
				SecondsPassed = 0;
				MinutesPassed += 1;
			} else{
				SecondsPassed += Time.deltaTime;
			}
			TimerText.text = MinutesPassed + ":" + SecondsPassed.ToString("F0");
		}
	}
	
	public void AddScore(int Value){
		CurrentScore += Value;
		ScoreText.text = "Score: " + CurrentScore;
	}
	
	public void ShowWinScreen(){
		EndScoreText.text = ScoreText.text;
		EndTimerText.text = TimerText.text;
		winner = true;
		StartCoroutine(FadeRoutine());
	}
	
	IEnumerator FadeRoutine(){
		while(percentFade < 1f){
			yield return new WaitForSeconds(0.01f);
			percentFade += .01f;
			winScreen.GetComponent<CanvasGroup>().alpha = percentFade;
		}
		Time.timeScale = 0;
	}
#endregion	
#region Pause Functions
	public void PauseGame(){
		PauseUI.SetActive(true);
		Time.timeScale = 0;
		Debug.Log("Paused");
		Cursor.lockState = CursorLockMode.None;
        manager.paused = true;
	}
	
	public void ResumeGame(){
		PauseUI.SetActive(false);
		Debug.Log("UnPaused");
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
        manager.paused = false;

	}
	
	public void GoToMenu(){
		Debug.Log("Menu");
		SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
        manager.paused = false;
	}
	
	public void ExitGame(){
		Debug.Log("Quit");
		Application.Quit();
	}
#endregion
}

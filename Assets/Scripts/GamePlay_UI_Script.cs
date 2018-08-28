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
	
	public int CurrentScore;
	public Text ScoreText;
#endregion

	void Start(){
		if (Instance == null){
			Instance = this;
		} else if (Instance != this){
			Destroy(this.gameObject);
		}
		
		SecondsPassed = 0.0f;
		MinutesPassed = 0;
		
		CurrentScore = 0;
		ScoreText.text = "Score: " + CurrentScore;
	}
	
	void Update(){
		UpdateTimeText();
		
		if(Input.GetKeyDown(KeyCode.P)){
			PauseGame();
		}
		
		if(Input.GetKeyDown(KeyCode.M)){
			AddScore(10);
		}
	}
	
	public void UpdateTimeText(){
		if(SecondsPassed >= 60){
			SecondsPassed = 0;
			MinutesPassed += 1;
		} else{
			SecondsPassed += Time.deltaTime;
		}
		
		TimerText.text = MinutesPassed + ":" + SecondsPassed.ToString("F0");
	}
	
	public void AddScore(int Value){
		CurrentScore += Value;
		ScoreText.text = "Score: " + CurrentScore;
	}
	
#region Pause Functions
	public void PauseGame(){
		PauseUI.SetActive(true);
		Time.timeScale = 0;
		Debug.Log("Paused");
		Cursor.lockState = CursorLockMode.None;
	}
	
	public void ResumeGame(){
		PauseUI.SetActive(false);
		Debug.Log("UnPaused");
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	public void GoToMenu(){
		Debug.Log("Menu");
		SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
	}
	
	public void ExitGame(){
		Debug.Log("Quit");
		Application.Quit();
	}
#endregion
}

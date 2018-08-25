using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_UI_Script : MonoBehaviour {
	
	//public String Level01Name;
	
	public void StartGame(){
		Debug.Log("Game Start");
		SceneManager.LoadScene("Level01", LoadSceneMode.Single);
	}
	
	public void QuitGame(){
		Debug.Log("Game Quit");
		Application.Quit();
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code fades the color of the objects material between two selected colors, back and forth, forever
public class Material_Change : MonoBehaviour {

	Renderer rend;
	
	public Color playerColor;
	[Header "Colors to switch between"]
	[Tooltip "Initial color"]
	public Color color1;
	[Tooltip "End color"]
	public Color color2;
	
	[Header "Time variables"]
	[Tooltip "Total duration, kinda this bigger w/ small multiplier for longer time to change"]
	public float time;
	[Tooltip "Change multiplier, decimals for slower"]
	public float multiplier;
	
	void Start(){
		rend = GetComponent<Renderer>();
	}
	
	void FixedUpdate(){
		playerColor = Color.Lerp(color1, color2, (Mathf.PingPong(Time.time,time) * multiplier));
	}
	
	void LateUpdate(){
		rend.material.SetColor("_Color", playerColor);
	}
}

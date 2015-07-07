using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin _skin = null;

	public Texture2D rulesBox;

	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;
	private float menuX;
	private bool showRules;
	private bool menuOn;

	void Start(){

		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.15f;
		buttonWidth = screenWidth * 0.4f;
		menuX = screenWidth * 0.3f;
		showRules = false;
		menuOn = true; 
	}

	void OnGUI () {
		Color bgc = GUI.backgroundColor;

		if (_skin != null) {
			GUI.skin = _skin;
		}
		if (menuOn) {        
			GUI.backgroundColor = Color.clear;

			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			GUI.Box (new Rect (menuX * 0.8f, 0.5f * buttonHeight, buttonWidth * 1.3f, 0.7f * screenHeight), "4 Gewinnt 3D");

			GUI.backgroundColor = bgc;

			if (GUI.Button (new Rect (menuX, 2f * buttonHeight, buttonWidth, buttonHeight), "Neues Spiel")) {
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect (menuX, 3.5f * buttonHeight, buttonWidth, buttonHeight), "Steuerung")) {
				menuOn = false;
				showRules = true;
			}
			if (GUI.Button (new Rect (menuX, 5.0f * buttonHeight, buttonWidth, buttonHeight), "Beenden")) {
				Application.Quit ();
			}
		}
		if(showRules){
			GUI.Label (new Rect(menuX,0, buttonWidth, buttonHeight), "Steuerung");
			GUI.DrawTexture(new Rect(0.1f*screenWidth,0.2f*screenHeight,0.8f*screenWidth, 0.8f*screenHeight), rulesBox);

			if(GUI.Button(new Rect (menuX+buttonWidth,0,buttonWidth*0.6f,buttonHeight), "schließen"))
			{	showRules=false;
				menuOn=true;
			}
		}
	}
}

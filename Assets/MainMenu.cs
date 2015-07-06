using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin _skin = null;

	public static Texture2D rulesBox;

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
		buttonHeight = screenHeight * 0.1f;
		buttonWidth = screenWidth * 0.4f;
		menuX = screenWidth * 0.3f;
		showRules = false;
		menuOn = true; 
	}

	void OnGUI () {

		if (_skin != null) {
			GUI.skin = _skin;
		}
		if (menuOn) {             
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			GUI.Box (new Rect (menuX * 0.8f, 2f * buttonHeight, buttonWidth * 1.3f, 0.6f * screenHeight), "4 Gewinnt 3D");
		
			if (GUI.Button (new Rect (menuX, 3f * buttonHeight, buttonWidth, buttonHeight), "Neues Spiel")) {
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect (menuX, 4.5f * buttonHeight, buttonWidth, buttonHeight), "Regeln")) {
				menuOn = false;
				showRules = true;
			}
			if (GUI.Button (new Rect (menuX, 6.0f * buttonHeight, buttonWidth, buttonHeight), "Beenden")) {
				Application.Quit ();
			}
		}
		if(showRules){
			GUI.DrawTexture(new Rect(0.1f*screenWidth,0.1f*screenHeight,0.8f*screenWidth, 0.8f*screenHeight), rulesBox);

			if(GUI.Button(new Rect (menuX,4.5f*buttonHeight,buttonWidth,buttonHeight), "close"))
			{	showRules=false;
				menuOn=true;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class winScene : MonoBehaviour {

	public GUISkin _skin = null;
	 

	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;
	
	void Start(){
		
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.1f;
		buttonWidth = screenWidth * 0.3f;
	}
	
	void OnGUI () {

		if (_skin != null) {
			GUI.skin = _skin;
		}

		GUI.Box (new Rect (buttonWidth,buttonHeight,buttonWidth, buttonHeight), "Glückwunsch du hast gewonnen");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(buttonWidth,2f*buttonHeight,buttonWidth,buttonHeight), "Haupmenü")) {
			Application.LoadLevel(0);
		}
		if(GUI.Button(new Rect(buttonWidth,3.5f*buttonHeight,buttonWidth,buttonHeight), "Neues Spiel")) {
		//	WinCheck.resetGame();
		}
		if(GUI.Button(new Rect(buttonWidth,5f*buttonHeight,buttonWidth,buttonHeight), "Beenden")) {
			Application.Quit();
		}
	}
}

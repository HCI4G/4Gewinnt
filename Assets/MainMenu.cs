using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin _skin = null;

	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;
	private float menuX;

	void Start(){

		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.1f;
		buttonWidth = screenWidth * 0.4f;
		menuX = screenWidth * 0.3f;

	}

	void OnGUI () {

		if (_skin != null) {
			GUI.skin = _skin;
		}
		                  

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(menuX,2f*buttonHeight,buttonWidth,buttonHeight), "Neues Spiel")) {
			MainUI.startGame = true;
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(menuX,3.5f*buttonHeight,buttonWidth,buttonHeight), "Beenden")) {
			Application.Quit();
		}
	}
}

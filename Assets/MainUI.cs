using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {

	public static int playerCount;
	public Texture2D rulesBox;

	public GUISkin _skin = null;
	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;
	private float menuX;
	private static int plr1;
	private static int plr2;
	private bool paused;
	private bool showRules;
	public static bool switchPlr;
	public static string player; 

	private float duration = 3; 

	public static bool win; 

	void Start(){
		Time.timeScale = 1.0f;
		playerCount = 1;
		plr1 = 0;
		plr2 = 0;
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.1f;
		buttonWidth = screenWidth * 0.4f;
		menuX = 0.3f * screenWidth;
		win = false;
		paused = false;
		switchPlr = false;
	}

	void PlayerWho(){
		if(playerCount%2==0)
			plr2++;
		else 
			plr1++;
	}

	void pauseMenu(){
		Time.timeScale = 0.0f;

		GUI.Box (new Rect (menuX*0.8f,2f*buttonHeight,buttonWidth*1.3f,0.7f*screenHeight),"Glückwunsch du hast gewonnen");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(menuX,3f*buttonHeight,buttonWidth,buttonHeight), "Haupmenü")) {
			Application.LoadLevel(0);
		}
		if(GUI.Button(new Rect(menuX,4.5f*buttonHeight,buttonWidth,buttonHeight), "Neues Spiel")) {		
			win = false;
			paused=false;
			WinCheck.getInstance().resetGame();
		}
		if(GUI.Button(new Rect(menuX,6f*buttonHeight,buttonWidth,buttonHeight), "Regeln")) {
			paused = false;
			showRules = true;
		}
		if(GUI.Button(new Rect(menuX,7.5f*buttonHeight,buttonWidth,buttonHeight), "Zurück")) {
			Time.timeScale = 1.0f;
			paused=false;

		}

	}


	void WinMenu () {

		GUI.Box (new Rect (menuX*0.8f,2f*buttonHeight,buttonWidth*1.3f,0.6f*screenHeight), "Glückwunsch du hast gewonnen");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(menuX,3f*buttonHeight,buttonWidth,buttonHeight), "Haupmenü")) {
			Application.LoadLevel(0);
		}
		if(GUI.Button(new Rect(menuX,4.5f*buttonHeight,buttonWidth,buttonHeight), "Neues Spiel")) {
			PlayerWho();
			win = false;
			
			WinCheck.getInstance().resetGame();
		}
		if(GUI.Button(new Rect(menuX,6f*buttonHeight,buttonWidth,buttonHeight), "Beenden")) {
			Application.Quit();
		}
		if (win)
			WinMenu ();
	}


	void PlayerOne(int windowID) {
		GUI.Label (new Rect(screenHeight*0.05f,30,40,50),""+plr1);
		}

	void PlayerTwo(int windowID) {
		GUI.Label (new Rect(screenHeight*0.05f,30,40,50),""+plr2);
	}


	void OnGUI(){
	
		//if (win)
		//	WinMenu ();

		if(GUI.Button(new Rect(0,0, screenWidth*0.2f, screenHeight * 0.1f), "Menu")){
			paused = true;
		}
		if(GUI.Button(new Rect(screenWidth*0.8f,0, screenWidth*0.2f, screenHeight * 0.1f), "Reset")){
			
		}
		GUI.Window(0,new Rect( screenWidth*0.25f, 0, screenWidth*0.2f, screenHeight * 0.1f),PlayerOne,"Player 1");
		GUI.Window(1,new Rect( screenWidth*0.55f, 0, screenWidth*0.2f, screenHeight * 0.1f),PlayerTwo,"Player 2");

		if (paused)
			pauseMenu ();

		if(showRules){
			GUI.DrawTexture(new Rect(0.1f*screenWidth,0.1f*screenHeight,0.8f*screenWidth, 0.8f*screenHeight), rulesBox);
			
			if(GUI.Button(new Rect (menuX,4.5f*buttonHeight,buttonWidth,buttonHeight), "close")){
				showRules=false;
				paused = true;
			}
		}
	}
}

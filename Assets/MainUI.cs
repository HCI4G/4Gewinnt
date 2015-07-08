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
	private bool showWinMenu;
	private string winner; 

	private float duration = 3; 

	public static bool win; 

	void Start(){
		Time.timeScale = 1.0f;
		playerCount = 1;
		plr1 = 0;
		plr2 = 0;
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.15f;
		buttonWidth = screenWidth * 0.4f;
		menuX = 0.3f * screenWidth;
		win = false;
		paused = false;
		showWinMenu = false;
	}

	void PlayerWho(){
		if (playerCount % 2 == 0) {
			plr1++;
		} else{ 
			plr2++;
		}
	}


	void WinnerWho(){
		if (playerCount % 2 == 0) {
			winner = "Spieler 1";
		} else{ 
			winner = "Spieler 2";
		}
	}


	void pauseMenu(){
		Time.timeScale = 0.0f;
		WinCheck.getInstance().windowMode = true;
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(menuX,1.5f*buttonHeight,buttonWidth,buttonHeight), "Haupmenü")) {
			Application.LoadLevel(0);
		}
		if(GUI.Button(new Rect(menuX,2.75f*buttonHeight,buttonWidth,buttonHeight), "Neues Spiel")) {		
			paused=false;
			WinCheck.getInstance().windowMode = false;
			WinCheck.getInstance().resetGame();
		}
		if(GUI.Button(new Rect(menuX,4.0f*buttonHeight,buttonWidth,buttonHeight), "Steuerung")) {
			paused = false;
			showRules = true;
		}
		if(GUI.Button(new Rect(menuX,5.25f*buttonHeight,buttonWidth,buttonHeight), "Zurück")) {
			Time.timeScale = 1.0f;
			paused=false;
			WinCheck.getInstance().windowMode = false;
		}

	}

	void WinnerMenu(){
		WinnerWho ();
		WinCheck.getInstance().windowMode = true;

		Color bc = GUI.backgroundColor;
		Color cc = GUI.backgroundColor;
		GUI.contentColor = Color.black;
		GUI.backgroundColor = Color.clear;
		GUI.Box (new Rect (0.80f*menuX, 1.25f * buttonHeight, 1.3f*buttonWidth, 0.5f*screenHeight), "Glückwunsch!!! \n" + winner + ", \n du hast das Spiel gewonnen");
		GUI.backgroundColor = bc;
		GUI.contentColor = cc;
		if (GUI.Button (new Rect (menuX, 3f * buttonHeight, buttonWidth, buttonHeight), "OK")) {
			PlayerWho();
			win = false;
			showWinMenu = true;
		}
	}

	void WinMenu () {


		GUI.Box (new Rect (menuX*0.8f,1.25f*buttonHeight,buttonWidth*1.3f,0.75f*screenHeight), "Wie geht es weiter?");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(menuX,2.25f*buttonHeight,buttonWidth,buttonHeight), "Haupmenü")) {
			Application.LoadLevel(0);
		}
		if(GUI.Button(new Rect(menuX,3.5f*buttonHeight,buttonWidth,buttonHeight), "Neues Spiel")) {
			showWinMenu = false;
			WinCheck.getInstance().windowMode = false;
			WinCheck.getInstance().resetGame();
		}
		if(GUI.Button(new Rect(menuX,4.75f*buttonHeight,buttonWidth,buttonHeight), "Beenden")) {
			Application.Quit();
		}
	}


	void MainMenu(){
		Color bgc = GUI.backgroundColor;
		Color highlight = new Color(0.7f,0.3f,0.5f,0.7f);
		GUI.Box(new Rect(0,0, screenWidth,0.125f*screenHeight), ":");

		GUI.backgroundColor = highlight;
		if(playerCount%2==0)
            GUI.Box(new Rect(screenWidth * 0.55f, 0.035f * screenHeight, screenWidth * 0.2f, screenHeight * 0.05f), "");
		else
            GUI.Box(new Rect(screenWidth * 0.27f, 0.035f * screenHeight, screenWidth * 0.2f, screenHeight * 0.05f), "");
		GUI.backgroundColor = bgc;

		GUI.Label(new Rect( screenWidth*0.27f, 0.035f*screenHeight, screenWidth*0.2f, screenHeight * 0.1f),"Spieler 1         "+plr1);
		GUI.Label(new Rect( screenWidth*0.55f, 0.035f*screenHeight, screenWidth*0.2f, screenHeight * 0.1f),plr2+"          Spieler 2");
		
		if(GUI.Button(new Rect(screenWidth*0.01f,0.032f*screenHeight, screenWidth*0.2f, screenHeight * 0.08f), "Menü")){
			paused = true;
		}
		if(GUI.Button(new Rect(screenWidth*0.79f,0.032f*screenHeight, screenWidth*0.2f, screenHeight * 0.08f), "Ausgangsrotation")){
            WinCheck.getInstance().resetRotation();
		}
	}

	void OnGUI(){

		if (_skin != null) {
			GUI.skin = _skin;
		}

		MainMenu ();

		if (win)
			WinnerMenu ();

		if (showWinMenu) 
			WinMenu();



		if (paused)
			pauseMenu ();

		if(showRules){
			GUI.Box(new Rect(0.1f*screenWidth,0.15f*screenHeight,0.8f*screenWidth, 0.8f*screenHeight), "Steuerung");
			GUI.DrawTexture(new Rect(0.2f*screenWidth,0.3f*screenHeight,0.6f*screenWidth, 0.6f*screenHeight), rulesBox);

			if(GUI.Button(new Rect (menuX+1.1f*buttonWidth,0.15f*screenHeight,0.4f*buttonWidth,0.8f*buttonHeight), "schließen")){
				showRules=false;
				paused = true;
				WinCheck.getInstance().windowMode = false;

			}
		}
	}
}

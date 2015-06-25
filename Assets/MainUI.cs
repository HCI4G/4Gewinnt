using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {

	public static int playerCount;

	public GUISkin _skin = null;
	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;
	private float menuX;


	public static bool startGame  = false;
	public static bool win; 

	void Start(){
		if (!startGame){
			Application.LoadLevel (1);
	}
		playerCount = 1;
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.1f;
		buttonWidth = screenWidth * 0.4f;
		menuX = 0.3f * screenWidth;
		win = false;
	}

	string PlayerWho(){
		string plr;
		if(playerCount%2==0)
			plr = "Player 2";
		else 
			plr = "Player 1";
		return plr;
	}

	
	void winMenu () {
		
		GUI.Box (new Rect (menuX,buttonHeight,buttonWidth, buttonHeight), "Glückwunsch du hast gewonnen");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(menuX,2f*buttonHeight,buttonWidth,buttonHeight), "Haupmenü")) {
			Application.LoadLevel(0);
		}
		if(GUI.Button(new Rect(menuX,3.5f*buttonHeight,buttonWidth,buttonHeight), "Neues Spiel")) {
			MainUI.startGame = true;
			win = false;
			
			WinCheck.getInstance().resetGame();
		}
		if(GUI.Button(new Rect(menuX,5f*buttonHeight,buttonWidth,buttonHeight), "Beenden")) {
			Application.Quit();
		}
		if (win)
			winMenu ();
	}


	void OnGUI(){
		if (startGame) {
			string player = PlayerWho ();
			GUI.Box (new Rect (0, 0, screenWidth, screenHeight * 0.1f), player);
		}
	}
}

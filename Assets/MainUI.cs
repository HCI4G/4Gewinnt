using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {

	public static int playerCount;

	public GUISkin _skin = null;
	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;

	public static bool startGame  = false;

	void Start(){
		if (!startGame){
			Application.LoadLevel (1);
	}
		playerCount = 1;
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.1f;
		buttonWidth = screenWidth * 0.3f;
	}

	string PlayerWho(){
		string plr;
		if(playerCount%2==0)
			plr = "Player 2";
		else 
			plr = "Player 1";
		return plr;
	}

	void OnGUI(){
		if (startGame) {
			string player = PlayerWho ();
			GUI.Box (new Rect (0, 0, screenWidth, screenHeight * 0.1f), player);
		}
	}
}

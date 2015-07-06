using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class OnMouseOverExample : MonoBehaviour 
{
	public GUISkin _skin = null;
    private GameObject[, ,] spheres;

    private int currentX;
    private int currentY;
    private int currentZ;
    private string currentKey;
    private List<string> victoryLinePostions;

	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;
	private float menuX;

    private WinCheck.SphereState currentActiveUserState;
    private Color color;
	
	void Start(){
		
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		buttonHeight = screenHeight * 0.15f;
		buttonWidth = screenWidth * 0.4f;
		menuX = screenWidth * 0.3f;
	}

	public Boolean doWindow0 = false;

	void DoWindow0() {

		GUI.Box (new Rect (menuX*0.8f,2f*buttonHeight,buttonWidth*1.3f,0.55f*screenHeight), "Richtige Kugel?");

		if(GUI.Button(new Rect(menuX,3f*buttonHeight,buttonWidth,buttonHeight), "Ja!")){
				doWindow0 = false;
				MainUI.playerCount++;
                
                if (WinCheck.getInstance().currentActiveUserState == WinCheck.SphereState.BLACK)
                {
                    WinCheck.getInstance().currentActiveUserState = WinCheck.SphereState.WHITE;
                }
                else
                {
                    WinCheck.getInstance().currentActiveUserState = WinCheck.SphereState.BLACK;
                }
			initialWinCheck();
			WinCheck.getInstance().windowMode = false;
		}

		if(GUI.Button(new Rect(menuX,4.25f*buttonHeight,buttonWidth,buttonHeight), "Nein!")){
			doWindow0 = false; 
			resetCurrentSphere();
			WinCheck.getInstance().windowMode = false;

		}
	}
	void OnGUI() {
		if (_skin != null) {
			GUI.skin = _skin;
		}

		if (doWindow0)
			DoWindow0 ();

	}
	
	public void OnMouseOver()
	{
		if (WinCheck.getInstance().windowMode)
			return;

		if(Input.GetMouseButtonDown(0)){
            currentActiveUserState = WinCheck.getInstance().currentActiveUserState;
            loadSpheresArray();
            findPositionInArray();

            if (WinCheck.statusMap[currentKey] != WinCheck.SphereState.NORMAL)
            {
                return;
            }
            
            changeActiveUser(currentActiveUserState);
            changeForm();
            
			if (!doWindow0){
				doWindow0 = true;  
				WinCheck.getInstance().windowMode = true;
			}
        }		  

	}

	private void resetCurrentSphere(){
		GetComponent<Renderer>().material.color = WinCheck.getInstance().normalState;
		WinCheck.SphereState state = WinCheck.getInstance().currentActiveUserState;
		changeStatus(WinCheck.SphereState.NORMAL);
		GetComponent<Renderer>().transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

    private void changeActiveUser(WinCheck.SphereState state)
    {
        Debug.Log("Before:" +GetComponent<Renderer>().material.color);
        if (state == WinCheck.SphereState.BLACK) { 
            GetComponent<Renderer>().material.color = Color.black;
            Debug.Log("Change state black");
           
            changeStatus(WinCheck.SphereState.BLACK);
            }
        else if(state == WinCheck.SphereState.WHITE){
            GetComponent<Renderer>().material.color = Color.white;           
            Debug.Log("Change state white");
            changeStatus(WinCheck.SphereState.WHITE);
        }
        Debug.Log("After:" +GetComponent<Renderer>().material.color);
    }



    private void changeForm()
    {
        GetComponent<Renderer>().transform.localScale = new Vector3(1, 1, 1);        
        
    }


    private void loadSpheresArray()
    {
        if (spheres == null)
        {
            spheres = WinCheck.getSpheresArray();
        }
           
    }

    private void initialWinCheck()
    {     
       if (checkConditions())
        {
            Debug.Log("You've won fucker!");
            //Change the color of the victory line spheres
            foreach (string s in victoryLinePostions)
            {
                int position = Convert.ToInt32(s);
                int x = position /100 % 10;
                int y = position / 10 % 10;
                int z = position % 10;
                GameObject sphere = spheres[x, y, z];
                sphere.GetComponent<Renderer>().material.color = Color.cyan;

            } 

			MainUI.win = true;
        }
    }
    private void changeStatus(WinCheck.SphereState state)
    {             
        WinCheck.statusMap[currentKey] = state;

    }

    private bool checkConditions()    {        
       
        if (checkHorizontal() || checkVertical() || checkDepth() || checkDiagonal())
        {
            return true;
        }
        return false; 
    }

    private void findPositionInArray()
    {               

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    GameObject sphere = spheres[x, y, z];
                    if ( this.name.Equals(sphere.name))
                        
                    {
                        currentX = x;
                        currentY = y;
                        currentZ = z;
                        currentKey = "" + x + y + z;
                    }  
                }

            }

        }
      
    }

    private bool checkHorizontal(){


        victoryLinePostions = new List<string>();
        List<string> downSpheres = new List<string>();
        List<string> upSpheres = new List<string>();
 
        WinCheck.SphereState state = WinCheck.statusMap[currentKey];
        //check down
        int x = currentX - 1;
        for (; x >= 0; x--)
        {
           string testPostion = "" + x + currentY + currentZ;
           downSpheres.Add(testPostion);
            //return false if the enum state differs
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            } 

        }

        //check up
        x = currentX + 1;
        for (; x < 4; x++)
        {          
            string testPostion = "" + x + currentY + currentZ;
            upSpheres.Add(testPostion);
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            } 
        }

        mergeLists(upSpheres, downSpheres);
       return true;
    }

    private bool checkVertical()
    {
        WinCheck.SphereState state = WinCheck.statusMap[currentKey];
        victoryLinePostions = new List<string>();
        List<string> downSpheres = new List<string>();
        List<string> upSpheres = new List<string>();

        //check down
        int z = currentZ - 1;
        for (; z >= 0; z--)
        {
            
            string testPostion = "" + currentX + currentY + z;
            downSpheres.Add(testPostion);
            //return false if the enum state differs
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            }
        }

        //check up
        z = currentZ + 1;
        for (; z < 4; z++)
        {
            string testPostion = "" + currentX + currentY + z;
            upSpheres.Add(testPostion);
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            }
        }
        mergeLists(upSpheres, downSpheres);
        return true;
    }

    private bool checkDepth()
    {
        WinCheck.SphereState state = WinCheck.statusMap[currentKey];
        victoryLinePostions = new List<string>();
        List<string> downSpheres = new List<string>();
        List<string> upSpheres = new List<string>();

        //check down
        int y = currentY - 1;
        for (; y >= 0; y--)
        {
            string testPostion = "" + currentX + y + currentZ;
            downSpheres.Add(testPostion);
            //return false if the enum state differs
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            }
        }

        //check up
        y = currentY + 1;
        for (; y < 4; y++)
        {
            string testPostion = "" + currentX + y + currentZ;
            upSpheres.Add(testPostion);
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            }
        }
        mergeLists(upSpheres, downSpheres);
        return true;
    }

    private bool checkDiagonal()
    {

        WinCheck.SphereState state = WinCheck.statusMap[currentKey];
        foreach (List<string> list in WinCheck.diagonalSphereLines)
        {
            bool winConditionSuccess = false;
            if (list.Contains(currentKey))
            {
                winConditionSuccess = true;
                foreach (string spherePostion in list)
                {
                    if (WinCheck.statusMap[spherePostion] != state)
                    {
                        winConditionSuccess = false;
                    }
                }
                if (winConditionSuccess)
                {
                    victoryLinePostions = list;
                    return true;
                }
            }
        }
        return false;
    }


    private void mergeLists(List<string> upper, List<string> lower)
    {
        foreach (string s in upper)
        {
            victoryLinePostions.Add(s);
        }
        victoryLinePostions.Add(currentKey);
        foreach (string s in lower)
        {
            victoryLinePostions.Add(s);
        }
        
    }

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class OnMouseOverExample : MonoBehaviour 
{

    private GameObject[, ,] spheres;

    private int currentX;
    private int currentY;
    private int currentZ;
    private string currentKey;
    private List<string> victoryLinePostions;
    
	public Boolean doWindow0 = false;

	void DoWindow0(int windowID) {
		if(GUI.Button(new Rect(10, 30, 80, 20), "Klar!!!!")){
			if (doWindow0){
				doWindow0 = false;
				MainUI.playerCount++;
			}
		}
	}
	void OnGUI() {
		if (doWindow0)
			GUI.Window(0,new Rect(Screen.height/3,Screen.width/3,Screen.height/3,Screen.width/3),DoWindow0,"Richtige Kugel???");
	}
	
	public void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0)){
            loadSpheresArray();
            findPositionInArray();
			GetComponent<Renderer>().material.color = Color.black;
            changeStatus(WinCheck.SphereState.BLACK);
            changeForm();
            initialWinCheck();
			if (!doWindow0)
				doWindow0 = true;       
        }
		if(Input.GetMouseButtonDown(1)){
            loadSpheresArray();
            findPositionInArray();
			GetComponent<Renderer>().material.color = Color.white;
            changeStatus(WinCheck.SphereState.WHITE);
            changeForm();
            initialWinCheck();
			if (!doWindow0)
				doWindow0 = true;
        }


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
			MainUI.startGame = false;
			Application.LoadLevel(2);

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
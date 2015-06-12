using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OnMouseOverExample : MonoBehaviour 
{

    private GameObject[, ,] spheres;

    private int currentX;
    private int currentY;
    private int currentZ;
    private string currentKey;
    
	public void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0)){
            findPositionInArray();
			GetComponent<Renderer>().material.color = Color.black;
            changeStatus(WinCheck.SphereState.BLACK);
            changeForm();
            initialWinCheck();
           
        }
		if(Input.GetMouseButtonDown(1)){
            findPositionInArray();
			GetComponent<Renderer>().material.color = Color.white;
            changeStatus(WinCheck.SphereState.WHITE);
            changeForm();
            initialWinCheck();
        }
	}

    private void changeForm()
    {
        GetComponent<Renderer>().transform.localScale = new Vector3(1, 1, 1);        
        
    }


    private void loadSpheresArray()
    {
        spheres = WinCheck.getSpheresArray();    
    }

    private void initialWinCheck()
    {
        if (spheres == null)
        {
            loadSpheresArray();
        }
        if (checkConditions())
        {
            Debug.Log("You've won fucker!");
        }


    }
    private void changeStatus(WinCheck.SphereState state)
    {             
        WinCheck.statusMap[currentKey] = state;

    }

    private bool checkConditions()
    {        
        //TODO Diagonal 
        if (checkHorizontal() || checkVertical() || checkDepth())
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
                    if (this.gameObject.GetInstanceID() == sphere.GetInstanceID())
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
               
        WinCheck.SphereState state = WinCheck.statusMap[currentKey];
        //check down
        int x = currentX - 1;
        for (; x > 0; x--)
        {
           string testPostion = "" + x + currentY + currentZ;
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
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            } 
        }
       return true;
    }

    private bool checkVertical()
    {
        WinCheck.SphereState state = WinCheck.statusMap[currentKey];
        //check down
        int z = currentZ - 1;
        for (; z > 0; z--)
        {
            string testPostion = "" + currentX + currentY + z;
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
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            }
        }
        return true;
    }

    private bool checkDepth()
    {
        WinCheck.SphereState state = WinCheck.statusMap[currentKey];
        //check down
        int y = currentY - 1;
        for (; y > 0; y--)
        {
            string testPostion = "" + currentX + y + currentZ;
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
            if (!(state == WinCheck.statusMap[testPostion]))
            {
                return false;
            }
        }
        return true;
    }

    private bool checkDiagonal()
    {
       
        if (currentX == 0 | currentX == 1)
        {
            //plane 0,3 can only have the ones arround
            if (currentY == 1 | currentY == 2)
            {
                if (currentZ == 1 | currentZ == 2)
                {
                    return false;
                }
                else
                {
                    checkDiagonalProcess();
                }
            }            
           
        }
        else
        {
            //plane 1,2 only can have the middle ones
            if ((currentY == 1 | currentY == 2) & (currentZ == 1 | currentZ == 2))
            {
                checkDiagonalProcess();
            }
            else
            {
                return false;
            }
        }
        

        return true;
    }


    //Wenn das aufgerufen wird sind die ungültigen Position schon aussortiert.
    private bool checkDiagonalProcess()
    {
        WinCheck.SphereState state = WinCheck.statusMap[currentKey];

        return true;
    }


}
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WinCheck : MonoBehaviour {

    public GameObject mainPlane;
    private static GameObject[,,] spheres;
    public static Dictionary<string, SphereState> statusMap = new Dictionary<string, SphereState>();
    public static List<List<string>> diagonalSphereLines;
    private  Quaternion baseQuaternion;
    private  Color normalState;
    private static WinCheck winCheck;
    public WinCheck.SphereState currentActiveUserState = WinCheck.SphereState.BLACK;

    public enum SphereState
    {
        NORMAL, BLACK, WHITE
    }

	// Use this for initialization
	void Start () {

        winCheck = this;

        if (Application.HasProLicense())
        {
            Debug.Log("Unity is pro version.");
        }
        else
        {
            Debug.Log("Unity is free version.");
        }

        spheres = new GameObject[4,4,4];       
        int counter = 0;

        foreach (GameObject g in getChildren(mainPlane))
        {
            if (g.name.Contains("Plane"))
            {
                spheres[counter,0,0] = g;
                counter++;
            }
           
        }


        for (counter = 0; counter < 4; counter++)
        {
            int num = 0;
            foreach (GameObject g in getChildren(spheres[counter, 0, 0]))
            {
               
                if (g.name.Contains("Line"))
                {
                    int row = 0;
                    spheres[counter, num, 0] = g;
                    foreach (GameObject sphere in getChildren(g))
                    {
                        if (sphere.name.Contains("Sphere"))
                        {
                            {                              
                                spheres[counter, num, row] = sphere;
                                row++;
                            }
                        }
                       
                    }
                    num++;
                }

            }

        }

        setInitialStates();
        GameObject locSphere = spheres[0, 0, 0];
        normalState = locSphere.GetComponent<Renderer>().material.color;
        baseQuaternion = mainPlane.transform.rotation;
        initSphereLines();
       
	}


    private void setInitialStates()
    {       
		statusMap.Clear ();
			for (int x = 0; x < 4; x++) {
				for (int y = 0; y < 4; y++) {
					for (int z = 0; z < 4; z++) {
						string key = "" + x + y + z;
						statusMap.Add (key, SphereState.NORMAL);
					}
				}
			}
    }

	public void resetGame(){
		mainPlane.transform.localPosition = new Vector3(-4.5f, 0, 10f); ;
		mainPlane.transform.rotation = baseQuaternion;
		resetAllSphereAttributes();
	}

 
	void Update () {
        
        if (Input.GetKey(KeyCode.Space))
        {
			resetGame();
        }
	}

    private ArrayList getChildren(GameObject gameObject)
    {
       
        ArrayList gs = new ArrayList();
        try
        {
            Transform[] ts = gameObject.GetComponentsInChildren<Transform>();
            if (ts == null)
                return gs;
            foreach (Transform t in ts)
            {
                gs.Add(t.gameObject);

            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);           
        } 
        return gs;
    }

    public static GameObject[, ,] getSpheresArray()
    {
        return spheres;
    }

    private void resetAllSphereAttributes()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    string key = "" + x + y + z;
                    statusMap[key] = SphereState.NORMAL;
                    GameObject temp = spheres[x, y, z];
                    temp.GetComponent<Renderer>().transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    temp.GetComponent<Renderer>().material.color = normalState;
                }

            }

        }        
    }    
   
    private void initSphereLines()
    {

        diagonalSphereLines = new List<List<string>>();
		string[] frontS0Up = { "000", "110", "220", "330" };
		string[] frontS1Up = { "001", "111", "221", "331" };
		string[] frontS2Up = { "002", "112", "222", "332" };
		string[] frontS3Up = { "003", "113", "223", "333" };
		
		string[] frontS0Down = { "030", "120", "210", "300" };
		string[] frontS1Down = { "031", "121", "211", "301" };
		string[] frontS2Down = { "032", "122", "212", "302" };
		string[] frontS3Down = { "033", "123", "213", "303" };

		string[] leftS0Up = { "003", "012", "021", "030" };      
		string[] leftS1Up = { "103", "112", "121", "130" };
		string[] leftS2Up = { "203", "212", "221", "230" };
		string[] leftS3Up = { "303", "312", "321", "330" };

		string[] leftS0Down = { "033", "022", "011", "000" };      
		string[] leftS1Down = { "133", "122", "111", "100" };
		string[] leftS2Down = { "233", "222", "211", "200" };
		string[] leftS3Down = { "333", "322", "311", "300" };
		
        string[] bottomS0Up = { "000", "101", "202", "303" };
		string[] bottomS1Up = { "010", "111", "212", "313" };
		string[] bottomS2Up = { "020", "121", "222", "323" };
		string[] bottomS3Up = { "030", "131", "232", "333" };
        
		string[] bottomS0Down = { "003", "102", "201", "300" };
		string[] bottomS1Down = { "013", "112", "211", "310" };
		string[] bottomS2Down = { "023", "122", "221", "320" };
		string[] bottomS3Down = { "033", "132", "231", "330" };

        string[] diagonal1 = { "300", "211", "122", "033" };
        string[] diagonal2 = { "303", "212", "121", "030" };
        string[] diagonal3 = { "330", "221", "112", "003" };
        string[] diagonal4 = { "333", "222", "111", "000" };
        
		addToList(frontS0Up);
		addToList(frontS1Up);
		addToList(frontS2Up);
		addToList(frontS3Up);
		
		addToList(frontS0Down);
		addToList(frontS1Down);
		addToList(frontS2Down);
		addToList(frontS3Down);

		addToList(leftS0Up);
		addToList(leftS1Up);
        addToList(leftS2Up);
        addToList(leftS3Up);

		addToList(leftS0Down);
		addToList(leftS1Down);
		addToList(leftS2Down);
		addToList(leftS3Down);

		addToList(bottomS0Up);
		addToList(bottomS1Up);
		addToList(bottomS2Up);
		addToList(bottomS3Up);
		
		addToList(bottomS0Down);
		addToList(bottomS1Down);
		addToList(bottomS2Down);
		addToList(bottomS3Down);

        addToList(diagonal1);
        addToList(diagonal2);
        addToList(diagonal3);
        addToList(diagonal4);

    }

    private void addToList(string[] list)
    {
        List<string> tempList = new List<string>();
        foreach (string s in list)
        {
            tempList.Add(s);
        }
        diagonalSphereLines.Add(tempList);
    }

    public static WinCheck getInstance()
    {
        return winCheck;
    }
}

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WinCheck : MonoBehaviour {

    public GameObject mainPlane;
    private static GameObject[,,] spheres;
    public static Dictionary<string, SphereState> statusMap = new Dictionary<string, SphereState>();
    public static List<List<string>> diagonalSphereLines;

    public enum SphereState
    {
        NORMAL, BLACK, WHITE
    }

	// Use this for initialization
	void Start () {

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
        initSphereLines();
       
	}


    private void setInitialStates()
    {       
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    string key = "" + x + y + z;
                    statusMap.Add(key, SphereState.NORMAL);
                }

            }

        }        
    }

    
	
	// Update is called once per frame
	void Update () {       
	
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

    //TODO Rest of the diagonales
    private void initSphereLines()
    {

        diagonalSphereLines = new List<List<string>>();
        string[] leftS1 = { "010", "111", "212", "313" };      
        string[] leftS2 = { "020", "121", "222", "323" };
        string[] leftS3 = { "030", "131", "232", "333" };
        string[] leftS4 = { "000", "101", "202", "303" };

        string[] rightS4 = { "300", "201", "102", "003" };
        string[] rightS3 = { "330", "231", "132", "033" };
        string[] rightS2 = { "320", "221", "122", "023" };
        string[] rightS1 = { "310", "211", "112", "013" };

        string[] leftF1 = { "301", "211", "121", "031" };
        string[] leftF2 = { "302", "212", "122", "032" };
        string[] leftF3 = { "303", "213", "123", "033" };
        string[] leftF4 = { "300", "210", "120", "030" };
        
        string[] rightF1 = { "331", "221", "111", "001" };
        string[] rightF2 = { "332", "222", "112", "002"};
        string[] rightF3 = { "333", "223", "113", "003" };
        string[] rightF4 = { "330", "220", "110", "000" };

        string[] diagonal1 = { "300", "211", "122", "033" };
        string[] diagonal2 = { "303", "212", "121", "030" };
        string[] diagonal3 = { "330", "221", "112", "003" };
        string[] diagonal4 = { "333", "222", "111", "000" };
        
        addToList(leftS1);
        addToList(leftS2);
        addToList(leftS3);
        addToList(leftS4);

        addToList(rightS1);
        addToList(rightS2);
        addToList(rightS3);
        addToList(rightS4);

        addToList(rightF1);
        addToList(rightF2);
        addToList(rightF3);
        addToList(rightF4);

        addToList(leftF1);
        addToList(leftF2);
        addToList(leftF3);
        addToList(leftF4);

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



 
}

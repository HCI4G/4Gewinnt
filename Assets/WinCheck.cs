using UnityEngine;
using System.Collections;
using System;

public class WinCheck : MonoBehaviour {

    public GameObject mainPlane;
    private GameObject[,,] spheres; 

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

       
	}


    private void printArray()
    {       
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    if (spheres[x, y, z] != null)
                    {
                        Debug.Log(""+x +","+ y +","+ z + " has content: " + spheres[x, y, z]);
                    }
                }

            }

        }        
    }

    
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Space))
        {
            printArray();

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
}

using UnityEngine;
using System.Collections;

public class OnMouseOverExample : MonoBehaviour 
{
	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0)){
			GetComponent<Renderer>().material.color = Color.black;
            changeForm();
        }
		if(Input.GetMouseButtonDown(1)){
			GetComponent<Renderer>().material.color = Color.white;
            changeForm();
        }
	}

    void changeForm()
    {
        GetComponent<Renderer>().transform.localScale = new Vector3(1, 1, 1);
        
    }
}
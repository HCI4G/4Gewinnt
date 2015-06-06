using UnityEngine;
using System.Collections;

public class OnMouseOverExample : MonoBehaviour 
{
    protected enum SphereState
    {
        NORMAL, BLACK, WHITE
    }

    private SphereState status = SphereState.NORMAL;

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0)){
			GetComponent<Renderer>().material.color = Color.black;
            status = SphereState.BLACK;
            changeForm();
        }
		if(Input.GetMouseButtonDown(1)){
			GetComponent<Renderer>().material.color = Color.white;
            status = SphereState.WHITE;
            changeForm();
        }
	}

    void changeForm()
    {
        GetComponent<Renderer>().transform.localScale = new Vector3(1, 1, 1);        
        
    }

    SphereState getState()
    {
        return status;
    }

}
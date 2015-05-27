using UnityEngine;
using System.Collections;

public class OnMouseOverExample : MonoBehaviour 
{
	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
			GetComponent<Renderer>().material.color = Color.red;
		if(Input.GetMouseButtonDown(1))
			GetComponent<Renderer>().material.color = Color.blue;
	}
}
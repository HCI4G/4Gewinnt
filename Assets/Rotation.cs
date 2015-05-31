using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public float sidewaySpeed = 0.5F;
    public float accelerationSpeed = 0.5F;
    public float rotateSpeed = 0.5F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 moveVector = new Vector3(rotateSpeed, 0, 0);

            transform.Rotate(moveVector);

            moveVector = new Vector3(0, -accelerationSpeed, 0);
            transform.Translate(moveVector);

           
        }

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 moveVector = new Vector3(-rotateSpeed, 0, 0);
            transform.Rotate(moveVector);

            moveVector = new Vector3(0, +accelerationSpeed, 0);
            transform.Translate(moveVector);

        }

	
         if (Input.GetKey(KeyCode.W))
        {
            Vector3 moveVector = new Vector3(0, 0, sidewaySpeed);
            transform.Translate(moveVector);

           
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rotationVector = new Vector3(0, -accelerationSpeed, 0);
            transform.Rotate(rotationVector);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 moveVector = new Vector3(0, 0, -sidewaySpeed);
            transform.Translate(moveVector);
                        
           
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rotationVector = new Vector3(0, accelerationSpeed, 0);
            transform.Rotate(rotationVector);
        }

	}

}

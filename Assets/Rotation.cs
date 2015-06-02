using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public float sidewaySpeed = 0.5F;
    public float accelerationSpeed = 0.5F;
    public float rotateSpeed = 0.5F;
    private Quaternion baseQuaternion;
    public GameObject mainForm;

	// Use this for initialization
	void Start () {

        Debug.Log(mainForm);
        baseQuaternion = mainForm.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 moveVector = new Vector3(rotateSpeed, rotateSpeed, rotateSpeed);

            transform.Rotate(moveVector);

          
           
        }

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 moveVector = new Vector3(-rotateSpeed, -rotateSpeed, -rotateSpeed);
            transform.Rotate(moveVector);

           

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

        //Reset
        if (Input.GetKey(KeyCode.Tab) | Input.GetMouseButton(2)){

            mainForm.transform.localPosition = new Vector3(-4.5f, 0, 10f); ;
            mainForm.transform.rotation = baseQuaternion;
           
        }


	}

}

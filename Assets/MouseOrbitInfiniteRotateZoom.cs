using UnityEngine;
using System.Collections;

[AddComponentMenu("Infinite Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitInfiniteRotateZoom : MonoBehaviour
{

    public Transform target;
    public float xSpeed = 12.0f;
    public float ySpeed = 12.0f;
    public float scrollSpeed = 10.0f;

    public float zoomMin = 1.0f;

    public float zoomMax = 20.0f;

    private float distance;

    public Vector3 position;

    private bool isActivatedLeftMouse;

    private bool isActivatedRightMouse;



    float x = 0.0f;

    float y = 0.0f;



    // Use this for initialization

    void Start()
    {

        Vector3 angles = transform.eulerAngles;

        x = angles.y/2;

        y = angles.x/2;




    }



    void LateUpdate()
    {


        // only update if the mousebutton is held down

        if (Input.GetMouseButtonDown(1))
        {

            isActivatedRightMouse = true;

        }

        // if mouse button is let UP then stop rotating camera

        if (Input.GetMouseButtonUp(1))
        {

            isActivatedRightMouse = false;

        }

        // only update if the mousebutton is held down

        if (Input.GetMouseButtonDown(0))
        {

            isActivatedLeftMouse = true;

        }

        // if mouse button is let UP then stop rotating camera

        if (Input.GetMouseButtonUp(0))
        {

            isActivatedLeftMouse = false;

        }



        if (target && isActivatedLeftMouse)
        {

            //  get the distance the mouse moved in the respective direction

            x += Input.GetAxis("Mouse X") * xSpeed;

            y -= Input.GetAxis("Mouse Y") * ySpeed;



            // when mouse moves left and right we actually rotate around local y axis	

            Vector3 newPosition = target.position;
            newPosition.Set(1.1f*x, 1.1f*x, 1.1f*x);

            //transform.RotateAround(newPosition, transform.up, x);

            //transform.RotateAround(newPosition, transform, x);
            transform.Rotate(newPosition);


            // when mouse moves up and down we actually rotate around the local x axis	

           // transform.RotateAround(newPosition, transform.right, y);



            // reset back to 0 so it doesn't continue to rotate while holding the button

            x = 0;

            y = 0;
            return;

        }


        if (target && isActivatedRightMouse)
        {

            //  get the distance the mouse moved in the respective direction

            x += Input.GetAxis("Mouse X") * xSpeed;

            y -= Input.GetAxis("Mouse Y") * ySpeed;



            // when mouse moves left and right we actually rotate around local y axis	

            Vector3 newPosition = target.position;
            newPosition.Set(8f, 8f, 8f);

            transform.RotateAround(newPosition, transform.up, x);

           
            // when mouse moves up and down we actually rotate around the local x axis	

            transform.RotateAround(newPosition, transform.right, y);



            // reset back to 0 so it doesn't continue to rotate while holding the button

            x = 0;

            y = 0;
            return;

        }
        

            // see if mouse wheel is used 	

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {

                // get the distance between camera and target

                float direction = Input.GetAxis("Mouse ScrollWheel");

                if (direction < 0 & scrollSpeed > 0)
                {
                    scrollSpeed = scrollSpeed * -1;
                }
                else if (direction > 0 & scrollSpeed < 0)
                {
                    scrollSpeed = scrollSpeed * -1;
                }
              
                distance = scrollSpeed;
           
                // position the camera FORWARD the right distance towards target

                position = -(transform.forward * distance) + target.position;
                
                // move the camera

                transform.position = position;

            }

       
       
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTestControl : MonoBehaviour
{
    public GameObject Ship;
    //public GameObject Camera;

    //Camera rotation vars
    public float mouseSensitivityX = 1f;
    public float mouseSensitivityY = 1f;
    Transform cameraTransform;

    float mousePosX = 0;
    float mousePosY = 0;

    void Awake()
    {
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    // Use this for initialization
    void Start()
    {

        mousePosX = 0;
        mousePosY = 0;
    }

    // Update is called once per frame
    void Update()
    {

        mousePosX = Input.GetAxis("Mouse X") + mousePosX /** mouseSensitivityX*/;
        mousePosY = -Input.GetAxis("Mouse Y") + mousePosY /** mouseSensitivityY*/;

        if (mousePosX > 160) { mousePosX = 160; }
        else if (mousePosX < -160) { mousePosX = -160; }

        if (mousePosY > 160) { mousePosY = 160; }
        else if (mousePosY < -160) { mousePosY = -160; }

        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        transform.Rotate(mousePosY / 100, mousePosX / 100, 0f);
        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime *.017f);
        //transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime *.017f);

        //Move ship up
        if (Input.GetKey(KeyCode.Space))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.up * 10000);
        }

        //Move ship down
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.up * -10000);
        }

        //Move ship forward 
        if (Input.GetKey(KeyCode.W))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.forward * 50000);
        }

        //Move ship backwards
        if (Input.GetKey(KeyCode.S))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.forward * -50000);
        }

        //Strafe ship left
        if (Input.GetKey(KeyCode.A))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.right * -10000);
        }

        //Strafe ship right
        if (Input.GetKey(KeyCode.D))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.right * 10000);
        }

        //Rotate ship right
        if (Input.GetKey(KeyCode.E))
        {
            Ship.GetComponent<Rigidbody>().AddTorque(transform.forward * -3000);
        }

        //Rotate ship right
        if (Input.GetKey(KeyCode.Q))
        {
            Ship.GetComponent<Rigidbody>().AddTorque(transform.forward * 3000f);
        }
        //otherObject.GetComponent(NameOfScript).enabled = false;
    }
}

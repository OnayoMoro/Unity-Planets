using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTestControl : MonoBehaviour
{
    public GameObject Ship;

    //Camera rotation vars
    public float mouseSensitivityX = 40;
    public float mouseSensitivityY = 20;
    float verticalLookRotation;
    float horizontalLookRotation;
    Transform cameraTransform;

    void Awake()
    {
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Look rotation:
        transform.Rotate(Vector3.forward * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime *.017f);
        transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime *.017f);

        //Move ship up
        if (Input.GetKey(KeyCode.Space))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.forward * 50000);
        }

        //Move ship down
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.forward * -50000);
        }

        //Move ship forward 
        if (Input.GetKey(KeyCode.W))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.up * 300000);
        }

        //Move ship backwards
        if (Input.GetKey(KeyCode.S))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.up * -50000);
        }

        //Strafe ship left
        if (Input.GetKey(KeyCode.A))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.right * 50000);
        }

        //Strafe ship right
        if (Input.GetKey(KeyCode.D))
        {
            Ship.GetComponent<Rigidbody>().AddForce(transform.right * -50000);
        }

        //Rotate ship right
        if (Input.GetKey(KeyCode.E))
        {
            Ship.GetComponent<Rigidbody>().AddTorque(transform.up * -30000);
        }

        //Rotate ship right
        if (Input.GetKey(KeyCode.Q))
        {
            Ship.GetComponent<Rigidbody>().AddTorque(transform.up * 30000f);
        }
        //otherObject.GetComponent(NameOfScript).enabled = false;
    }
}

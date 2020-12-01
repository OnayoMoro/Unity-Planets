using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject Player;
    public GameObject Ship;
    public GameObject Camera;

    bool pressed = true;
    Vector3 player_pos = new Vector3(0f, 0.57f, -0.03f);
    Vector3 player_rotation = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && pressed == true)
        {
            pressed = false;

            //Deactivate sctipt or player?
            Player.GetComponent<PlayerBasicControl>().enabled = false;

            //Ship control enabled 
            Ship.GetComponent<ShipTestControl>().enabled = true;

            //Disable ship gravity interaction and add gimble control (drag)
            Ship.GetComponent<Attractor>().enabled = false;
            Ship.GetComponent<Rigidbody>().drag = 2;
            Ship.GetComponent<Rigidbody>().angularDrag = .4f;

            //Change camera to ship view
            Camera.transform.parent = null;
        }

        else if (pressed == false)
        {
            Vector3 moveCamTo_pos = Ship.transform.localPosition - Ship.transform.forward * 20f + Ship.transform.up * 5f;

            float bias = .96f;

            Camera.transform.position = Camera.transform.position * bias + moveCamTo_pos * (1f - bias);
            Camera.transform.LookAt(Ship.transform.localPosition + Ship.transform.up * 10f + Ship.transform.forward + Ship.transform.right);
            Quaternion ting = new Quaternion(Ship.transform.rotation.x, Ship.transform.rotation.y, Ship.transform.rotation.z, Ship.transform.rotation.w);
            Camera.transform.rotation = ting;

            if (Input.GetKeyDown(KeyCode.F) && pressed == false)
            {
                pressed = true;

                //Deactivate scipt or player?
                Player.GetComponent<PlayerBasicControl>().enabled = true;

                //Ship control disabled 
                Ship.GetComponent<ShipTestControl>().enabled = false;

                //Enable ship gravity interaction disable gimple control (drag)
                Ship.GetComponent<Attractor>().enabled = true;
                Ship.GetComponent<Rigidbody>().drag = 0;
                Ship.GetComponent<Rigidbody>().angularDrag = 0.05f;

                //Change camera to player view
                Camera.transform.parent = Player.transform;
                Camera.transform.localPosition = player_pos;
                Camera.transform.localEulerAngles = player_rotation;
            }
        }

    }
}

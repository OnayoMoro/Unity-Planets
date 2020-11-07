using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEnvironment : MonoBehaviour
{
    public Camera PlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera.backgroundColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

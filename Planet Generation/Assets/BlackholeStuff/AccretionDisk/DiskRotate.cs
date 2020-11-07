using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskRotate : MonoBehaviour
{
    public float horizontal_speed = 500f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, horizontal_speed, 0f) * Time.deltaTime);
    }
}

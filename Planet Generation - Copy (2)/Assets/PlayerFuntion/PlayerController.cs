using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 4;
    public float jumpForce = 40;
    public LayerMask groundedMask;
 
    // System vars
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    // Calculate movement:
        // float inputX = Input.GetAxisRaw("Horizontal");
        // float inputY = Input.GetAxisRaw("Vertical");
 
        // Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
        // Vector3 targetMoveAmount = moveDir * walkSpeed;
        // moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);
 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump")) {
            if (grounded) {
                GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            }
        }
 
        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
 
        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
            grounded = true;
        }
        else {
            grounded = false;
        }
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
 
    }
    void FixedUpdate() {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
    }
}
 
    


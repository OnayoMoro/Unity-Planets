using UnityEngine;
using System.Collections;
 
public class PlayerBasicControll : MonoBehaviour {
 
    // public vars
    public float mouseSensitivityX = 250;
    public float mouseSensitivityY = 250;
    public float walkSpeed = 4;
    public float jumpForce = 40;
    public LayerMask groundedMask;
 
    // Tangial velocity stuff
    public static GameObject FoundObject;
    public static string RaycastReturn;

    // System vars
    public static bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    Transform cameraTransform;
 
 
    void Awake() {
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
    }
 
    void Update() {
 
        // Look rotation:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation,-90,90);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
 
        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
 
        Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);
 
        // Jump
        if (Input.GetButtonDown("Jump")) {
            if (grounded) {
                GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);

                // Carry out tangial velocity here?


            }
        }
 
        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
 
        if (Physics.Raycast(ray, out hit, 1 + .2f, groundedMask)) {
            grounded = true;
            RaycastReturn = hit.collider.gameObject.name;
            //Debug.Log(RaycastReturn);
            FoundObject = GameObject.Find(RaycastReturn);
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
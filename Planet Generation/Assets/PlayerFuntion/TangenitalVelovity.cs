// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// /*
// Sources
// https://www.youtube.com/watch?v=9QDKnQm_poI&ab_channel=TimothyPalladino
// https://answers.unity.com/questions/830550/physics-compensating-for-movingrotating-terrain.html
// https://www.dummies.com/education/science/physics/calculating-tangential-velocity-on-a-curve/
// */

// public class TangenitalVelovity : MonoBehaviour
// {
//     public Transform direction;
//     public float Distance;
//     public float Speed;
//     public float AngularSpeed;
//     public float tangenitalVelocity;
//     public float planetPosVelocity;
//     GameObject FoundObject;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (PlayerBasicControll.grounded == true){
//             Debug.Log("Object Name: " + PlayerBasicControll.RaycastReturn);
//             if (PlayerBasicControll.RaycastReturn.Contains("mesh")){

//                 // need to change way to find parent object
//                 string currentObject = "Planet";
//                 FoundObject = GameObject.Find(currentObject);

//                 Speed = FoundObject.GetComponent<Rigidbody>().velocity.magnitude; // speed
                
//                 Vector3 val = FoundObject.GetComponent<Rigidbody>().angularVelocity;
//                 AngularSpeed = val.magnitude * Mathf.Rad2Deg;    // radians/s

//                 // distance/radius to centre of object
//                 Distance = Vector3.Distance(gameObject.transform.position, FoundObject.transform.position); 

//                 // tangenital velocity
//                 tangenitalVelocity = Distance * AngularSpeed;
//                 planetPosVelocity = Mathf.Cos(tangenitalVelocity);

//                 gameObject.GetComponent<Rigidbody>().AddForce(direction.forward * planetPosVelocity);
//                 //gameObject.GetComponent<Rigidbody>().AddForce(val);
//             }
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    public Transform player;
    public Transform planet;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustRotation();
    }

    void AdjustRotation(){
        Vector3 gravityUpDir = (player.position - planet.transform.position).normalized;
        Vector3 playerUp = player.up;

        Quaternion targetRotation = Quaternion.FromToRotation(playerUp,gravityUpDir) * player.rotation;
        player.rotation = Quaternion.Slerp(player.rotation,targetRotation,50 * Time.deltaTime);
        //Debug.Log("Rotation Corrected");
    }
}

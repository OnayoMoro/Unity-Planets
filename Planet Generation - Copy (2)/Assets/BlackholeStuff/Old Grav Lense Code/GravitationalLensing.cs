using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GravitationalLensing : MonoBehaviour
{
    //public settings
    public Shader shader;
    public Transform blackhole;
    public float ratio; //responsible for aspect ratio of screen
    public float radius; //set size of black hole in units

    //private settings
    Camera cam;
    Material _material; //will be generated procedurally

    void Update(){
        ratio = 1f / cam.aspect;
    }

    //a way of accessing the material and making sure it is created
    //at any given time
    Material material {
        get{
            if (_material == null){
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    } 

    //will happen whenever the camera is activated
    void OnEnable(){
        cam = GetComponent<Camera> ();
        ratio = 1f / cam.aspect; //height over width
    }

    //when camera off, disable/destroy material (save memory)
    void OnDisable(){
        if (_material){
            DestroyImmediate (_material);
        }
    }

    Vector3 wtsp;
    Vector2 pos;
    
    //if blackhole is within camera view, apply shader to it
    //when camera takes image of the scene it becomes the "source"
    //passess to destination processed image
    void OnRenderImage(RenderTexture source, RenderTexture  destination){
        //processing happens here
        if (shader && material && blackhole){
            wtsp = cam.WorldToScreenPoint(blackhole.position); 
            //world to screen point
            //position of any obeject on the screen
            
            //is the blackhole in front of the camera
            if(wtsp.z > 0){
                //normalising position, shaders like to work with
                //normalised vectors
                pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);

                //apply shader parameters
                _material.SetVector("_Position", pos);
                _material.SetFloat("_Ratio", ratio);
                _material.SetFloat("_Radius", radius);
                _material.SetFloat("_Distance", Vector3.Distance(blackhole.position, transform.position));

                //apply shader to image
                //takes in view image from camera and applies
                //material for blackhole shader, returning a new 
                //modified image
                Graphics.Blit(source, destination, material);
            }
        }
    }


}

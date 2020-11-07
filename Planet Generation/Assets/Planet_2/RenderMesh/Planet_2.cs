using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GeneratePlanet();
    }
    //Create 6 mesh filters for displaying terrain faces
    [Range(2,256)]
    public int resolution;
    public bool autoUpdate = true;
    public enum FaceRenderMask {All, Top, Bottom, Left, Right, Front, Back};
    public FaceRenderMask faceRenderMask;

    public ShapeSettings_planet2 shapeSettings;
    public ColourSettings_planet2 colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator_planet2 shapeGenerator = new ShapeGenerator_planet2();
    ColourGenerator_planet2 colourGenerator = new ColourGenerator_planet2();

    [SerializeField, HideInInspector]
    MeshFilter[] meshfilters;
    TerrainFace_planet2[] terrainFaces;     

    void LOD()
    {
        
    }   

    void Initialise()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colourSettings);

        if (meshfilters == null || meshfilters.Length == 0)
        {
            meshfilters = new MeshFilter[6];    
        }

        terrainFaces = new TerrainFace_planet2[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)
        {
            if (meshfilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshfilters[i] = meshObj.AddComponent<MeshFilter>();
                meshfilters[i].sharedMesh = new Mesh();
            }
            meshfilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace_planet2(shapeGenerator, meshfilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask-1 == i;
            meshfilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void GeneratePlanet()
    {
        Initialise();
        GenerateMesh();
        GenerateColours(); 
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialise();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialise();
            GenerateColours();  
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshfilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colourGenerator.UpdateColours();
    }
}

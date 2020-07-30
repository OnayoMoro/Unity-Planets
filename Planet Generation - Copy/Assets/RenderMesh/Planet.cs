using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject player;   
    //Create 6 mesh filters for displaying terrain faces
    [Range(2,256)]
    public int resolution;
    public static int size = 1;
    public bool autoUpdate = true;
    public enum FaceRenderMask {All, Top, Bottom, Left, Right, Front, Back};
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colourGenerator = new ColourGenerator();

    Chunk  chunk;

    [SerializeField, HideInInspector]
    MeshFilter[] meshfilters;
    TerrainFace[] terrainFaces;   

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        

        GeneratePlanet();
        //Debug.Log(Vector3.Distance(position.normalized * shapeSettings.planetRadius, Planet.player.position));

        StartCoroutine(PlanetGenerationLoop());
    }

    private IEnumerator PlanetGenerationLoop()
    {
        Debug.Log("PlanetGenerationLoop");
        while(true)
        {
            yield return new WaitForSeconds(1f);
            GeneratePlanet();
        }
    }

    public static Dictionary<int, float> detailLevelDistances = new Dictionary<int, float>()
    {
        {0, Mathf.Infinity},
        {1, 200f},
        {2, 130f},
        {3, 90f},
        {4, 50f},
        {5, 23f},
        {6, 10f},
        {7, 5f},
        {8, 1f}
    };

    //Help me pls

    void Initialise()
    {
        Debug.Log("Initialise");
        shapeGenerator.UpdateSettings(ref shapeSettings);
        colourGenerator.UpdateSettings(ref colourSettings);

        if (meshfilters == null || meshfilters.Length == 0)
        {
            meshfilters = new MeshFilter[6];    
        }

        terrainFaces = new TerrainFace[6];

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

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshfilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask-1 == i;
            meshfilters[i].gameObject.SetActive(renderFace);
        }
    }


// aka I am god - Yen
    public void GeneratePlanet()
    {
        Debug.Log("Generate Planet");
        Initialise();
        GenerateMesh();
        GenerateColours(); 
    }

    public void OnShapeSettingsUpdated()
    {
        Debug.Log("OnShapeSettingsUpdate");
        if (autoUpdate)
        {
            Initialise();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        Debug.Log("OnColourSettingsUpdated");
        if (autoUpdate)
        {
            Initialise();
            GenerateColours();  
        }
    }

    void GenerateMesh()
    {
        Debug.Log("GenerateMesh");
        for (int i = 0; i < 6; i++)
        {
            if (meshfilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructTree(ref player);
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {

        // C O L O U R S
        Debug.Log("GenerateColours");
        colourGenerator.UpdateColours();
    }
}

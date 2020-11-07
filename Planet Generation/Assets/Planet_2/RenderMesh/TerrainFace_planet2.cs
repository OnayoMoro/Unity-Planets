using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace_planet2
{
    Mesh mesh;
    int resolution;
    Vector3 localup;
    Vector3 axisA, axisB; 
    ShapeGenerator_planet2 shapeGenerator;


    public TerrainFace_planet2(ShapeGenerator_planet2 shapeGenerator, Mesh mesh, int resolution, Vector3 localup)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localup = localup;
        this.shapeGenerator = shapeGenerator;

        axisA = new Vector3(localup.y, localup.z, localup.x);
        axisB = Vector3.Cross(localup, axisA);
    }

    //Draw triangles
    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution*resolution];
        int[] triangles = new int[(resolution-1)*(resolution-1)*6];   //E.g. resolution = 4, faces = (resolution-1)squared, triangles = (faces)*2, vertices = (triangles)*3
        int triIndex = 0;

        //Used to define where the vertex should be along the face
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x+y*resolution; //Entire row of vertices * resolution (tracks itterations)
                Vector2 percent = new Vector2(x,y) / (resolution-1);
                Vector3 pointOnUnitCube = localup + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                //Don't draw past the last vertices
                if (x!=resolution-1&&y!=resolution-1)       
                {
                    //Calculate vertices to draw two triangles
                    triangles[triIndex] = i;
                    triangles[triIndex+1] = i+resolution+1;
                    triangles[triIndex+2] = i+resolution;
                    
                    triangles[triIndex+3] = i;
                    triangles[triIndex+4] = i+1;
                    triangles[triIndex+5] = i+resolution+1;

                    triIndex += 6;                    
                }
            }
        }
        mesh.Clear();   //In case of resolution change, don't want to reference indicies that don't exist anymore
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

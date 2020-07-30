using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk 
{
    ShapeSettings shapeSettings;
    ShapeGenerator shapeGenerator = new ShapeGenerator();
    Planet planet;
    Chunk[] children;
    Chunk parent;
    Vector3 position;
    float radius;
    int detailLevel;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public Chunk(Chunk[] children, Chunk parent, Vector3 position, float radius, int detailLevel, Vector3 localUp, Vector3 axisA, Vector3 axisB)
    {
        this.children = children;
        this.parent = parent;
        this.position = position;
        this.radius = radius;
        this.detailLevel = detailLevel;
        this.localUp = localUp;
        this.axisA = axisA;
        this.axisB = axisB;
    }

    public void GenerateChildren(GameObject player)
    {
        //planet.player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("GenerateChildren");
        if (detailLevel <=8 && detailLevel >= 0)
        {
            //Saved in vars for temporary debuging
            var a = Planet.size;
            var b = position.normalized;
            var c = a * b;
            var d = player.transform.position;
            float distance = Vector3.Distance(c,d);
            //Debug.Log(distance);
            if (distance <= Planet.detailLevelDistances[detailLevel])
            {
                children = new Chunk[4];
                children[0] = new Chunk(new Chunk[0], this, position + axisA * radius / 2 + axisB * radius / 2, radius / 2, detailLevel + 1, localUp, axisA, axisB);
                children[1] = new Chunk(new Chunk[0], this, position + axisA * radius / 2 - axisB * radius / 2, radius / 2, detailLevel + 1, localUp, axisA, axisB);
                children[2] = new Chunk(new Chunk[0], this, position - axisA * radius / 2 + axisB * radius / 2, radius / 2, detailLevel + 1, localUp, axisA, axisB);
                children[3] = new Chunk(new Chunk[0], this, position - axisA * radius / 2 - axisB * radius / 2, radius / 2, detailLevel + 1, localUp, axisA, axisB);

                foreach (Chunk child in children)
                {
                    child.GenerateChildren(player);
                }
            }
        }
    }

    public Chunk[] GetVisableChildren()
    {
        Debug.Log("GetVisableChildren");
        List<Chunk> toBeRendered = new List<Chunk>();

        if (children.Length > 0)
        {
            foreach (Chunk child in children)
            {
                toBeRendered.AddRange(child.GetVisableChildren());
                Debug.Log("There are children");
            }
        }
        else 
        {
            toBeRendered.Add(this);
        }

        return toBeRendered.ToArray();
    }

    //Draw triangles
    public (Vector3[], int[]) CalculateVerticiesAndTriangles(int triangleOffset)
    {
        Debug.Log("Calculate Verticies and Triangles");
        int planetRadius = 1;
        int resolution = 8;
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
                Vector3 pointOnUnitCube = localUp + ((percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB) * planetRadius;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized * planetRadius;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                //Don't draw past the last vertices
                if (x!=resolution-1&&y!=resolution-1)       
                {
                    //Calculate vertices to draw two triangles
                    triangles[triIndex] = i + triangleOffset;
                    triangles[triIndex+1] = i + resolution + 1 + triangleOffset;
                    triangles[triIndex+2] = i + resolution + triangleOffset;
                    
                    triangles[triIndex+3] = i + triangleOffset;
                    triangles[triIndex+4] = i + 1 + triangleOffset;
                    triangles[triIndex+5] = i + resolution + 1 + triangleOffset;

                    triIndex += 6;                    
                }
            }
        }
        return (vertices, triangles);
        // mesh.Clear();   //In case of resolution change, don't want to reference indicies that don't exist anymore
        // mesh.vertices = vertices;
        // mesh.triangles = triangles;
        // mesh.RecalculateNormals();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    float radius;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA, axisB; 
    ShapeGenerator shapeGenerator;
    ShapeSettings shapeSettings;

    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localup)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localup;
        this.shapeGenerator = shapeGenerator;

        axisA = new Vector3(localup.y, localup.z, localup.x);
        axisB = Vector3.Cross(localup, axisA);
    }

    public void ConstructTree(ref GameObject player)
    {
        Debug.Log("ConstructTree");
        vertices.Clear();
        triangles.Clear();

        Chunk parent = new Chunk(null, null, localUp.normalized * Planet.size, radius, 0, localUp, axisA, axisB);
        parent.GenerateChildren(player);

        int triangleOffset = 0;
        foreach (Chunk child in parent.GetVisableChildren())
        {
            (Vector3[], int[]) verticiesAndTriangles = child.CalculateVerticiesAndTriangles(triangleOffset);
            vertices.AddRange(verticiesAndTriangles.Item1);
            triangles.AddRange(verticiesAndTriangles.Item2);
            triangleOffset += verticiesAndTriangles.Item1.Length;
        }
        
        bool pass;

        try
        {
            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            pass = true;
        }
        catch { pass = false; }

        if (pass) { Debug.Log("Pass"); }
        else { Debug.Log("Fail"); }
    }
}

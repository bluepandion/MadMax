//
// Auto-generate UV based on vertex positions
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVAutoTile : MonoBehaviour
{
    private Mesh mesh;
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;

        for (int i=0; i < vertices.Length; i++) {
            Vector3 v = vertices[i];            
            uv[i].x = v.x * transform.localScale.x;
            uv[i].y = v.z * transform.localScale.z;
        }

        mesh.uv = uv;
    }
    
    void Update()
    {
        
    }
}

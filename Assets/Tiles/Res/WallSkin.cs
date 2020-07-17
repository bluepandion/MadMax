using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkin : MonoBehaviour
{
    public Mesh wallMesh;
    public Mesh angledWallMesh;
    public Mesh slopeWallMesh;
    public Mesh bridgeWallMesh;
    public Mesh cornerMesh;
    public Material wallMaterial;
    public Material terrainMaterial;
    public Material blockMaterial;
    public Material lavaMaterial;

    void Start()
    {
        this.enabled = false;
    }

    void Update()
    {
    }
}

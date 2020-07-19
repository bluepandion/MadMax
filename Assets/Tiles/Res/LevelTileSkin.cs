using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelTileSkin : ScriptableObject
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

    void Awake()
    {
        Debug.Log("LevelTileSkin Awake()");
    }
}

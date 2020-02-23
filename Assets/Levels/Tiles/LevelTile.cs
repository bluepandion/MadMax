using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class LevelTile : MonoBehaviour
{
    private Vector3 snap = new Vector3(10f, 5f, 10f);

    void Start()
    {
        
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject)) {
            Vector3 pos = transform.localPosition;
            pos.x = Mathf.Floor(pos.x / snap.x) * snap.x;
            pos.y = Mathf.Floor(pos.y / snap.y) * snap.y;
            pos.z = Mathf.Floor(pos.z / snap.z) * snap.z;
            transform.localPosition = pos;
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileSnap : MonoBehaviour
{
    private const float snapX = 10f;
    private const float snapY = 5f;
    private const float snapZ = 10f;

    void Start()
    {
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            if (transform.hasChanged)
            {
                Vector3 pos = transform.localPosition;
                pos.x = Mathf.Floor(pos.x / snapX) * snapX;
                pos.y = Mathf.Floor(pos.y / snapY) * snapY;
                pos.z = Mathf.Floor(pos.z / snapZ) * snapZ;
                transform.localPosition = pos;
                transform.hasChanged = false;
            }
        }
    }
}

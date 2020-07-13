using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class Level : MonoBehaviour
{
    private Vector3 center = new Vector3(0f, 0f, 0f);

    void Start()
    {

    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject)) {
            transform.localPosition = center;
        }
    }
}

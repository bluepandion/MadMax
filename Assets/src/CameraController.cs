﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!target) {
            return;
        }
        Vector3 newPos;
        newPos = Vector3.Lerp(transform.position, target.position, speed);
        transform.position = newPos;
    }
}

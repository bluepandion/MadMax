﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{    
    public float acceleration = 8.0f;
    public float maxSpeed = 30.0f;
    public float turnAcceleration = 5.0f;
    public float turnMaxSpeed = 5.0f;
    public float turnSlowDown = 0.99f;
    public UnityEngine.UI.Text debugText;
    
    private int layerMask = ~(1 << 8);    
    private Rigidbody rb;
    private float defaultDrag = 3.0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
        defaultDrag = rb.drag;
    }
    
    void Update()
    {                
        float turn = Input.GetAxis("Horizontal");
        float breaks = Input.GetAxis("Vertical");                        
        
        RaycastHit hitGround;
        float onGround = 0.0f;
        if (Physics.Raycast(transform.position, 
            transform.up * -1f, 
            out hitGround, 1.0f, 
            layerMask)) 
        {
            onGround = 1.0f;
        }
                        
        //
        // Steering
        //
        float turnV = turn * turnAcceleration;
        turnV *= onGround;

        rb.AddTorque(0.0f, turnV, 0.0f, ForceMode.Acceleration);
        if (rb.angularVelocity.magnitude > turnMaxSpeed) {
            rb.angularVelocity *= (turnMaxSpeed / 
            rb.angularVelocity.magnitude);
        }
        //
        // Force car steering if speed drops below a certain amount
        // This is in case the car gets stuck
        //
        float forceRotate = 1.0f - (rb.velocity.magnitude / maxSpeed);
        forceRotate = Mathf.Pow(forceRotate, 2.0f);
        forceRotate *= turn;        
        transform.Rotate(0.0f, forceRotate, 0.0f);

        //
        // Faked acceleration damping caused by turning
        //
        float turnSpeedMultiplier = 
            1.0f - (rb.angularVelocity.magnitude / turnMaxSpeed) * turnSlowDown;
        
        //
        // Engine acceleration
        //
        float a = acceleration * rb.mass * onGround * turnSpeedMultiplier;
        Vector3 accelerationVector = transform.forward * a;

        rb.AddForce(accelerationVector, ForceMode.Acceleration);                        
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity *= (maxSpeed / rb.velocity.magnitude);
        }

        //
        // Flying disables drag
        //
        rb.drag = defaultDrag * onGround;

        debugText.text = rb.velocity.magnitude.ToString() + 
            "\n" + rb.drag.ToString() +
            "\n" + rb.angularVelocity.magnitude.ToString() +
            "\n" + forceRotate.ToString();
    }
}

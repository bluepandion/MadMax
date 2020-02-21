﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{    
    public float acceleration = 8.0f;
    public float maxSpeed = 40.0f;
    public float turnAcceleration = 250.0f;
    public float turnMaxSpeed = 0.8f;
    public float turnSlowDown = 0.1f;
    
    public float springDamper = 2.0f;
    

    public UnityEngine.UI.Text debugText;
    
    private int layerMask = ~(1 << 8);    
    private Rigidbody rb;
    private float defaultDrag = 3.0f;

    private Transform model;
    private Transform wheelFR;
    private Transform wheelFL;
    private Transform wheelRR;
    private Transform wheelRL;

    //private float wheelRadius = 1.0f;

    private Transform body;
            
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
        defaultDrag = rb.drag;
        //InitWheels();
                
        body = transform.Find("Body");
        if (!body) {
            Debug.Log("Car body not found!");
        }
    }

    void InitWheels() {
        Transform modelParent;
        Transform model;
        Transform wheelParent;
        modelParent = transform.Find("Model");
        if (!modelParent) {
            Debug.Log("Car model node not found.");
        }
        model = modelParent.transform.GetChild(0);
        if (!model) {
            Debug.Log("Car model not found.");
        }
        wheelParent = model.transform.Find("Wheels");
        if (!wheelParent) {
            Debug.Log("Car wheels parent 'Wheels' not found.");
        }
        wheelFR = wheelParent.Find("WheelFrontR");
        wheelFL = wheelParent.Find("WheelFrontL");
        wheelRR = wheelParent.Find("WheelRearR");
        wheelRL = wheelParent.Find("WheelRearL");
        if (!wheelFR) {
            Debug.Log("Car WheelFrontR not found");
        }
        if (!wheelFL) {
            Debug.Log("Car WheelFrontL not found");
        }
        if (!wheelRR) {
            Debug.Log("Car WheelRearR not found");
        }
        if (!wheelRL) {
            Debug.Log("Car WheelRearL not found");
        }
        Debug.Log("Car wheels-init done.");
    }
    
    void Update()
    {           
        float turn = Input.GetAxis("Horizontal");
        float breaks = Input.GetAxis("Vertical");                        
        
        RaycastHit hitGround;
        float onGround = 0.0f;
        Vector3 p = new Vector3(0f, -.1f, 0f);
        if (Physics.Raycast(transform.position + transform.up * 0.1f,
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
        float a = acceleration * onGround * turnSpeedMultiplier; //* rb.mass
        Vector3 accelerationVector = transform.forward * a;

        rb.AddForce(accelerationVector, ForceMode.Acceleration);                        
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity *= (maxSpeed / rb.velocity.magnitude);
        }
        
        Vector3 vDown = new Vector3(0f, -50.0f * (1.0f - onGround), 0f);
        rb.AddForce(vDown, ForceMode.Acceleration);

        //
        // Flying disables drag
        //
        rb.drag = defaultDrag * onGround;
                        
        if (debugText) {
            debugText.text = 
            rb.velocity.magnitude.ToString();            
                /*
                rb.velocity.magnitude.ToString() + 
                "\n" + rb.drag.ToString() +
                "\n" + rb.angularVelocity.magnitude.ToString() +
                "\n" + forceRotate.ToString();
                */
        }
    }

    
}

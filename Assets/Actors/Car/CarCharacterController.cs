﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCharacterController : MonoBehaviour
{    
    public float acceleration = 8.0f;
    public float maxSpeed = 40.0f;
    public float turnAcceleration = 250.0f;
    public float turnMaxSpeed = 360f;    
    public float turnSlowDown = 10f;
    public float turnFriction = 0.8f;
    public float airControl = 0.2f;
    public float gravity = 9.81f;
    
    public float springDamper = 2.0f;

    public Transform gun;
    private PlayerGun gunComponent;

    public UnityEngine.UI.Text debugText;
    
    private int layerMask = ~(1 << 8);    
    
    private CharacterController cc;
    
    private Transform model;
    private Transform wheelFR;
    private Transform wheelFL;
    private Transform wheelRR;
    private Transform wheelRL;

    private Vector3 velocity;
    private float turnTorque;
    
    //private float wheelRadius = 1.0f;

    private Transform body;
            
    void Start()
    {
        cc = GetComponent<CharacterController>();                
        
        if (gun)
        {
            gunComponent = gun.GetComponent<PlayerGun>();
            if (!gunComponent)
            {
                Debug.Log("Gun doesn't have a PlayerGun component");
            }
        } else {
            Debug.Log("Gun not found");
        }
                
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
        HandlePhysics();
        HandleShooting();        
    }

    private void HandleShooting() {
        float shoot = Input.GetAxis("Fire1");
        if (shoot > 0f) 
        {
            gunComponent.Shoot(gun.rotation);
        }
    }

    private void HandlePhysics() {
        float turn = Input.GetAxis("Horizontal");
        float breaks = Input.GetAxis("Vertical");
        
        float onGround = 0.0f;
        if (cc.isGrounded) {
            onGround = 1.0f;
            velocity.y = 0f;
        }
        
        Vector3 groundNormal;        
        RaycastHit hitGround;
        Vector3 p = new Vector3(0f, -.25f, 0f);
        if (Physics.Raycast(body.transform.position,
            transform.up * -.25f, 
            out hitGround, 
            0.4f, 
            layerMask)) 
        {
            groundNormal = hitGround.normal;            
            transform.up -= (transform.up - hitGround.normal) * 0.1f;
            onGround = 1.0f;
        } else {
            transform.up -= (transform.up - new Vector3(0f, 1f, 0f)) * 0.1f;            
            onGround = 0.0f;
        }
                                       
        //
        // Steering
        //        
        turnTorque *= Mathf.Clamp(1.0f - turnSlowDown * Time.deltaTime, 0f, 1f);
        float turnV = turn * turnAcceleration * Time.deltaTime;
        turnV *= (airControl + onGround * (1f - airControl));
        turnTorque += turnV;
        turnTorque = Mathf.Clamp(turnTorque, -turnMaxSpeed, turnMaxSpeed);
        body.transform.Rotate(0f, turnTorque * Time.deltaTime, 0f);
                
        //
        // Friction 
        //        
        velocity.x *= Mathf.Clamp(1f - (1f - onGround) * .5f * Time.deltaTime, 0f, 1f);
        velocity.z *= Mathf.Clamp(1f - (1f - onGround) * .5f * Time.deltaTime, 0f, 1f);

        velocity.x *= Mathf.Clamp(1f - (onGround) * 1f * Time.deltaTime, 0f, 1f);
        velocity.z *= Mathf.Clamp(1f - (onGround) * 1f * Time.deltaTime, 0f, 1f);

        //
        // Acceleration
        //
        Vector3 hVelocity;
        hVelocity.x = velocity.x;        
        hVelocity.y = 0f;
        hVelocity.z = velocity.z;
        if (hVelocity.magnitude > maxSpeed) {
            hVelocity *= (maxSpeed / hVelocity.magnitude);
        }
        velocity.x = hVelocity.x;
        velocity.z = hVelocity.z;
        //
        // Faked acceleration damping caused by turning
        //        
        float turnSpeedMultiplier = 1.0f - (Mathf.Abs(turnTorque) / turnMaxSpeed) * turnFriction;
        float a = acceleration * onGround * turnSpeedMultiplier;
        Vector3 accelerationVector = body.transform.forward * a;                        
        accelerationVector.y -= gravity;

        velocity += accelerationVector * Time.deltaTime;
        
        cc.Move(velocity * Time.deltaTime);
        
        velocity.x -= (velocity.x - cc.velocity.x) * 0.1f;
        velocity.z -= (velocity.z - cc.velocity.z) * 0.1f;
                        
        if (debugText) {
            debugText.text = 
            " vector      " + body.transform.forward.ToString() +
            "\n velocity    " + velocity.ToString() +
            "\n cc velocity " + cc.velocity.ToString() +
            "\n grounded    " + onGround.ToString();        
        }
    }
}

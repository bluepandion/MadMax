using System;
using System.Collections;
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
    public GameObject menu;

    private int layerMask = ~(1 << 8);

    private CharacterController cc;

    private Transform model;
    private Transform wheelFR;
    private Transform wheelFL;
    private Transform wheelRR;
    private Transform wheelRL;

    private Vector3 velocity;
    private float turnTorque;

    private float softener = 0.1f / (1f / 60f);

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

    void Update()
    {
    }

    public void Shoot()
    {
        gunComponent.Shoot(gun.rotation);
    }

    public void HandlePhysics(float turn, float breaks)
    {
        float onGround = 0.0f;
        if (cc.isGrounded) {
            onGround = 1.0f;
            velocity.y = 0f;
        }

        Vector3 groundNormal;
        RaycastHit hitGround;
        Vector3 p = new Vector3(0f, -.25f, 0f);
        if (Physics.Raycast(body.transform.position,
            transform.up * -1f,
            out hitGround,
            0.4f,
            layerMask))
        {
            groundNormal = hitGround.normal;
            transform.up -= (transform.up - hitGround.normal) * softener * Time.deltaTime;
            onGround = 1.0f;
        } else {
            transform.up -= (transform.up - new Vector3(0f, 1f, 0f)) * softener * Time.deltaTime;
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

        velocity.x -= (velocity.x - cc.velocity.x) * softener * Time.deltaTime;
        velocity.z -= (velocity.z - cc.velocity.z) * softener * Time.deltaTime;

        if (debugText)
        {
            debugText.text =
            " vector      " + body.transform.forward.ToString() +
            "\n velocity    " + velocity.ToString() +
            "\n cc velocity " + cc.velocity.ToString() +
            "\n grounded    " + onGround.ToString();
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        Transform o = hit.transform;
        Player p = transform.parent.GetComponent<Player>();
        if (p)
        {
            p.state.EnterZone(o);
        }
        //if (o.GetComponent("Lava")) {
        //    Debug.Log("Player hit lava");
        //}

    }

    public void Melt()
    {
        cc.Move(new Vector3(0f, -1f, 0f) * Time.deltaTime);
    }
}


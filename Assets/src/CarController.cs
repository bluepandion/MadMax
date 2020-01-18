using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(InputManager))]

public class CarController : MonoBehaviour
{
    //public InputManager in;
    public float speed = 1.0f;
    public float turnSpeed = 1.0f;
    public float turnSlowDown = 0.6f;
    public UnityEngine.UI.Text debugText;
    
    private int layerMask = ~(1 << 8);    
    private Rigidbody rb;
    private float defaultDrag = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
        defaultDrag = rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0.5) {
            Debug.Log("Forward");
            
        }        
        float s = speed * rb.mass;
        float turn = Input.GetAxis("Horizontal");        
        float slowDown = 1.0f - (Mathf.Abs(turn) * turnSlowDown);        
        float turnV = turn * (rb.velocity.magnitude + speed) * turnSpeed;
        
        RaycastHit hitGround;
        float onGround = 0.0f;
        if (Physics.Raycast(transform.position, transform.up * -1f, out hitGround, 1.0f, layerMask)) {
            onGround = 1.0f;
        }

        rb.drag = defaultDrag * onGround;

        debugText.text = rb.velocity.magnitude.ToString() + "\n" + rb.drag.ToString();

        transform.Rotate(0.0f, turnV, 0.0f);                

        rb.AddForce(transform.forward * 
            (s * turnSlowDown) *
            onGround
            ,ForceMode.Acceleration);                        
        
    }
}

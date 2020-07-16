using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berserker : MonoBehaviour
{
    private CharacterController bc;
    private Transform playerTransform;
    private Vector3 velocity;
    public float moveSpeed = 10f;
    public float friction = 0.9f;
    public float gravity = 9f;
    private bool playerEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<CharacterController>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEnter) {
            Vector3 v = (playerTransform.position - transform.position).normalized * moveSpeed;
            v.y = 0f;
            if (!bc.isGrounded)
            {
                velocity.x *= friction;
                velocity.z *= friction;
                velocity.y -= gravity * Time.deltaTime;
            } else {
                velocity.x = v.x;
                velocity.z = v.z;
                velocity.y = 0f;
            }

            bc.Move(velocity * Time.deltaTime);        
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Berserker detecting");
        if (other.gameObject.GetComponent<CarCharacterController>())
        {
            Debug.Log("Beserker detected player");
            transform.LookAt(bc.transform);
            playerTransform = other.transform;

            playerEnter = true;
        }
    }
}

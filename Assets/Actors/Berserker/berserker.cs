using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berserker : MonoBehaviour
{
    private CharacterController bc;
    private Transform playerTransform;
    public GameObject explosion;
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

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (
            hit.gameObject.GetComponent<CarCharacterController>() ||
            hit.gameObject.GetComponent<Bullet>() ||
            hit.gameObject.GetComponent<Lava>() ||
            hit.gameObject.GetComponent<EnemyTower>()
            )
        {
            DestroyObj();
        }
    }

    void DestroyObj() {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject); // destroy the berserker
        Destroy(expl, 2); // delete the explosion after 3 seconds

    }
}

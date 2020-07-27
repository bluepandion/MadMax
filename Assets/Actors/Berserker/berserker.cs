using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Berserker : EnemyBody
{
    private CharacterController bc;
    private Transform playerTransform;
    //public GameObject explosion;
    private Vector3 velocity;
    public float moveSpeed = 10f;
    public float friction = 0.9f;
    public float gravity = 9f;
    private bool playerEnter = false;
    private bool selfDestruct = false;
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

    public void PlayerEnter(Collider other)
    {
        //Debug.Log("Berserker detecting");
        
    }

    void OnTriggerEnter(Collider hit) {
        if (
            hit.gameObject.GetComponent<CarCharacterController>() ||
            hit.gameObject.GetComponent<Lava>() ||
            hit.gameObject.GetComponent<EnemyTower>() ||
            (hit.gameObject.tag == "Player-Bullet")
            )
        {
            //Debug.Log("Beserker being hit");
            if (!selfDestruct) {
                selfDestruct = true;
                gameObject.GetComponent<EnemyBody>().SelfDestruct(0.1f);
            }
        }
    }

    public override void HandleDetection (Collider other, Transform currentTransform)
    {
        Debug.Log("Berserker : EnemyBody :: HandleDetection()");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Beserker detected player");
            transform.LookAt(other.transform);
            playerTransform = other.transform;
            playerEnter = true;
            return;
        }
    }
}

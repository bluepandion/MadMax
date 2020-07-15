using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    private int layerMask = (1 << 8);
    private GameObject player;
    private Vector3 targetPosition;
    private bool playerEnter = false;
    private Transform playerTransform;
    private float speed = 0.05f;

    private PlayerGun gunComponent;
    // Start is called before the first frame update
    void Start()
    {
        gunComponent = GetComponent<PlayerGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform) {
            transform.LookAt(playerTransform.position);
        }
        
        if (playerEnter) {
            Shoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tower detecting");
        if (other.gameObject.GetComponent<CarCharacterController>())
        {
            Debug.Log("Tower detected player");
            playerEnter = true;
            Shoot();
            playerTransform = other.transform;
            targetPosition = playerTransform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit detecting");
        if (other.gameObject.GetComponent<CarCharacterController>())
        {
            Debug.Log("Tower detected player");
            playerEnter = false;
        }

    }

    private void Shoot() {
        Debug.Log("Enemy shoot");
        var step = speed * Time.deltaTime;

        if (transform && playerTransform) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerTransform.rotation, step);
            Quaternion rot = transform.rotation;
            Debug.Log(rot);
            gunComponent.Shoot(rot);

        }
    }
}

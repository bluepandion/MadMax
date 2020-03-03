using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int layerMask = (1 << 8);
    private int dectectRadius = 12;
    private GameObject player;
    private Vector3 targetPosition;

    public Transform enemy;

    private PlayerGun gunComponent; 
    // Start is called before the first frame update
    void Start()
    {
        gunComponent = GetComponent<PlayerGun>();
        DetectPlayer(transform.position, dectectRadius);      
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer(transform.position, dectectRadius);
        transform.LookAt(targetPosition);
    }

    void DetectPlayer(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Shoot();
            Debug.Log(hitColliders[i].name);
            player = GameObject.Find(hitColliders[i].name);
            targetPosition = player.transform.position;
            i++;
        }
    }

    private void Shoot() {
        Debug.Log("Enemy shoot");
        Quaternion rotation = Quaternion.FromToRotation(targetPosition, transform.forward);
        Debug.Log(rotation);
        gunComponent.Shoot(rotation);
    }
}

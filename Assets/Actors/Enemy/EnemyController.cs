using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int layerMask = (1 << 8);

    public Transform enemy;

    private PlayerGun gunComponent; 
    // Start is called before the first frame update
    void Start()
    {
        gunComponent = GetComponent<PlayerGun>();      
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemy(transform.position, 8);
    }

    void DetectEnemy(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Shoot();
            Debug.Log(hitColliders[i].name);
            i++;
        }
    }

    private void Shoot() {
        Debug.Log("Enemy shoot");
        gunComponent.Shoot(transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorCollider : MonoBehaviour
{
    private EnemyBody enemyBody;
    private GameObject target;

    void Start()
    {
        if (transform.parent)
        {
            transform.parent.gameObject.TryGetComponent(out enemyBody);
        }
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!target)
        {
            target = other.gameObject;
            if (enemyBody)
            {
                Debug.Log("Detector on enter");
                enemyBody.HandleDetection(target, gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemyBody)
        {
            Debug.Log("Detector on exit");
            enemyBody.HandleExitDetection(target, gameObject);
        }
        if (other.gameObject == target)
        {
            target = null;
        }

    }
}

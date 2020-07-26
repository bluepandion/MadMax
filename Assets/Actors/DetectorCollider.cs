using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorCollider : MonoBehaviour
{
    private EnemyBody enemyBody;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent) 
        {            
            transform.parent.gameObject.TryGetComponent(out enemyBody);             
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (enemyBody)
        {
            Debug.Log("Detector on enter");
            enemyBody.HandleDetection(other, transform);
        }        
    }

    private void OnTriggerExit(Collider other) 
    {
        if (enemyBody)
        {
            Debug.Log("Detector on exit");
            enemyBody.HandleExitDetection(other, transform);
        }        
    }


}

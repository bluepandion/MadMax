using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelfDestruct (float time) {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        Destroy(expl, 2);
        if (transform.parent) {
            Destroy(transform.parent.gameObject, time);
        }
        else if (gameObject) {
            Destroy(gameObject, time);
        }
    }
    public virtual void HandleDetection (Collider other, Transform currentTransform)
    {
        Debug.Log("EnemyBody :: HandleDetection()");        
    }
    public virtual void HandleExitDetection (Collider other, Transform currentTransform)
    {
        Debug.Log("EnemyBody :: HandleExitDetection()");        
    }
}

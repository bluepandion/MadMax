using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 240f;
    public float life = 1.0f;
    
    private Vector3 direction;
    private float lifeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0f, 0f, speed);
        lifeTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > life) {
            Kill();
        }
        transform.position += (transform.rotation * direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        GameObject g = other.gameObject;
        Kill();
    }

    public void Kill() 
    {
        Destroy(gameObject);
        Debug.Log("Destroy bullet");
    }
}

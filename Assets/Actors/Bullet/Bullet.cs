using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 240f;
    public float life = 1.0f;

    private float lifeTime = 0f;
    private Rigidbody rb;

    private Vector3 speedVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifeTime = 0f;
        if (rb) {
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.AddForce(speedVector, ForceMode.Impulse);
        }
    }

    public void SetAngle(Quaternion a)
    {
        Vector3 direction = new Vector3(0f, 0f, speed);
        speedVector = (a * direction);
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > life) {
            Kill();
        }
        //transform.position += (transform.rotation * direction * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision c)//Collider other)
    {
        //GameObject g = other.gameObject;
        Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

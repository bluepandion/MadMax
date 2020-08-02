using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public int health;
    public GameObject explosion;
    private bool selfDestruct = false;

    void Start()
    {
    }

    void Update()
    {
    }

    public void SelfDestruct (float time) {
        if (!selfDestruct)
        {
            selfDestruct = true;
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 2);
            if (gameObject)
            {
                Destroy(gameObject, time);
            }
        }
    }

    public virtual void Damage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    public virtual void HandleDetection (GameObject other, GameObject detector)
    {
        //Debug.Log("EnemyBody :: HandleDetection()");
    }

    public virtual void HandleExitDetection (GameObject other, GameObject detector)
    {
        //Debug.Log("EnemyBody :: HandleExitDetection()");
    }

}

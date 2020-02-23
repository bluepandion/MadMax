using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float shootDelay = 0.5f;
    public int clipSize = 5;
    public float clipDelay = 1.0f;

    public GameObject bullet;

    private float shootTimer = 0f;
    private float clipTimer = 0f;
    private int clip = 0;
    
    void Start()
    {
        shootTimer = shootDelay;
        clipTimer = clipDelay;
        clip = clipSize;
    }

    
    void Update()
    {
        shootTimer += Time.deltaTime;        
        if (clip == 0)        
        {
            clipTimer += Time.deltaTime;
            if (clipTimer >= clipDelay)
            {
                Reload();
            }
        }
    }

    public void Shoot(Quaternion gunAngle)
    {
        if (clip == 0) 
        {
            return;
        }
        if (shootTimer >= shootDelay) 
        {
            if (bullet)
            {                
                GameObject b = Instantiate(bullet, transform.position, gunAngle);
                Debug.Log("Shoot");                                
            }
            
            shootTimer = 0f;
            clip--;            
            if (clip == 0)
            {
                clipTimer = 0f;
            }
        }
    }

    public void Reload()
    {
        clip = clipSize;
        shootTimer = shootDelay;
    }
}

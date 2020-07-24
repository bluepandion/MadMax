using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilTower : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject flash;
    private Vector3 spawnLocation;
    private bool playerEnter = false;
    private bool striked = true;
    private int layerMask = (1 << 9);
    private Transform playerTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CarCharacterController>())
        {
            Debug.Log("CoilTower detected player");
            playerTransform = other.transform;

            float offset = 15f;
            float minX = playerTransform.position.x - offset;
            float maxX = playerTransform.position.x + offset;
            float minZ = playerTransform.position.z - offset;
            float maxZ = playerTransform.position.z + offset;
            float groundY;
            float randomX = Random.Range(minX,maxX);
            float randomZ = Random.Range(minZ,maxZ);
            Vector3 flashSpawn = new Vector3 (randomX, other.transform.position.y, randomZ);

            Vector3 world = new Vector3 (0f, -1f, 0f);
            RaycastHit hitGround;
            if (Physics.Raycast( (flashSpawn + new Vector3(0f, 100f, 0f)),
                world,
                out hitGround,
                150f,
                layerMask))
            {
                if (hitGround.collider) {
                    groundY = hitGround.point.y;
                    Debug.Log("ground y : " + groundY);
                    spawnLocation = new Vector3(randomX, groundY, randomZ);

                    FlashHandler();
                }    
            }

            playerEnter = true;
        }
    }

    void FlashHandler() {
        if (spawnLocation != null) {
            GameObject currentFlash = Instantiate(flash, spawnLocation , Quaternion.identity);
            Destroy(currentFlash, 1);
        }
    }

    
}

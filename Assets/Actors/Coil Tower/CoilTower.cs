using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilTower : EnemyBody
{
    public GameObject flash;
    private Vector3 spawnLocation;
    private bool playerEnter = false;
    private bool striked = true;
    private int layerMask = (1 << 9);
    private Transform playerTransform;

    void Start()
    {
    }

    void Update()
    {
    }

    private void PlayerEnter(GameObject other)
    {
        if (other.tag == "Player")
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
            if (Physics.Raycast((flashSpawn + new Vector3(0f, 100f, 0f)),
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

    //when bullet hit tower
    private void OnTriggerEnter(Collider collision) {
        Debug.Log("Coil collision");
        if (collision.gameObject.tag == "Player-Bullet") {
            Debug.Log("Coil being shot");
            gameObject.GetComponent<EnemyBody>().SelfDestruct(1f);
        }
    }

    public override void HandleDetection (GameObject other, GameObject detector)
    {
        Debug.Log("Coil : EnemyBody :: HandleDetection()");
        if (other.tag == "Player")
        {
            Debug.Log("Coil detected player");
            PlayerEnter(other);
            return;
        }
    }

    void FlashHandler() {
        if (spawnLocation != null) {
            GameObject currentFlash = Instantiate(flash, spawnLocation , Quaternion.identity);
            Destroy(currentFlash, 1);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : EnemyBody
{
    private int layerMask = (1 << 8);
    private GameObject player;
    public GameObject towerGun;
    private Vector3 targetPosition;
    private bool playerEnter = false;
    private Transform playerTransform;
    private float speed = 0.05f;

    private PlayerGun gunComponent;
    // Start is called before the first frame update
    void Start()
    {
        gunComponent = towerGun.GetComponent<PlayerGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform) {
            towerGun.transform.LookAt(playerTransform.position);
        }
        
        if (playerEnter) {
            Shoot();
        }
    }

    public override void HandleExitDetection (Collider other, Transform currentTransform)
    {
        Debug.Log("Exit detected");
        if (other.gameObject.GetComponent<CarCharacterController>())
        {
            Debug.Log("Player on exit tower");
            playerEnter = false;
        }

    }

    //when bullet hit tower
    private void OnTriggerEnter(Collider collision) {
        Debug.Log("Tower collision");
        if (collision.gameObject.tag == "Player-Bullet") {
            Debug.Log("Tower being shot");
            gameObject.GetComponent<EnemyBody>().SelfDestruct(0.1f);
        }
    }

    public override void HandleDetection (Collider other, Transform currentTransform)
    {
        Debug.Log("Tower : EnemyBody :: HandleDetection()");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Tower detected player");
            playerEnter = true;
            Shoot();
            playerTransform = other.transform;
            targetPosition = playerTransform.position;
            return;
        }
    }

    private void Shoot() {
        //Debug.Log("Tower shoot()");
        var step = speed * Time.deltaTime;

        if (towerGun && playerTransform) {
            towerGun.transform.rotation = Quaternion.RotateTowards(towerGun.transform.rotation, playerTransform.rotation, step);
            Quaternion rot = towerGun.transform.rotation;
            //Debug.Log("Tower shooting: " + gunComponent);
            gunComponent.Shoot(rot);

        }
    }
}

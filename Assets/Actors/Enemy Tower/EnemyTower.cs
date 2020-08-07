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
    private StateMachine sm;
    // Start is called before the first frame update
    void Start()
    {
        gunComponent = towerGun.GetComponent<PlayerGun>();
        sm = new StateMachine();
        sm.SetState(new StateIdle(this));
    }

    // Update is called once per frame
    void Update()
    {
        sm.Update();
        if (playerTransform) {
            towerGun.transform.LookAt(playerTransform.position);
        }

        if (playerEnter) {
            Shoot();
        }
    }

    public void CheckHealth()
    {
        if (health < 1)
        {
            Debug.Log("EnemyTower died");
            sm.SetState(new StateDestroying(this));
        }
    }

    public override void HandleExitDetection (GameObject other, GameObject detector)
    {
        Debug.Log("Exit detected");
        if (other.GetComponent<CarCharacterController>())
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

    public override void HandleDetection (GameObject other, GameObject detector)
    {
        Debug.Log("Tower : EnemyBody :: HandleDetection()");
        if (other.tag == "Player")
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

    public class StateIdle : IState
    {
        private EnemyTower owner;

        public StateIdle (EnemyTower o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("EnemyTower enter state idle");
        }
        void IState.Update()
        {
            if (owner.playerEnter)
            {
                owner.sm.SetState(new StateShooting(owner));
            }
            owner.CheckHealth();
        }
        void IState.Exit() {}
    }

    public class StateShooting : IState
    {
        private EnemyTower owner;

        public StateShooting (EnemyTower o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("EnemyTower enter state shooting");
        }

        void IState.Update()
        {
            owner.CheckHealth();
        }

        void IState.Exit()
        {
        }
    }

    public class StateDestroying : IState
    {
        private EnemyTower owner;

        public StateDestroying (EnemyTower o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("EnemyTower enter state destroying");
            owner.SelfDestruct(0f);
        }

        void IState.Update()
        {
        }

        void IState.Exit()
        {
        }
    }
}

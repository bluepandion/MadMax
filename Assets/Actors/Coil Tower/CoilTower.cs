using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilTower : EnemyBody
{
    public GameObject flash;
    private Vector3 spawnLocation;
    private bool playerEnter = false;
    private bool flashedDone = true;
    private bool striked = true;
    private int layerMask = (1 << 9);
    private Transform playerTransform;
    private StateMachine sm;
    private Flash flashComponent;

    public float flashDelay = 0.5f;
    private float flashTimer = 0f;
    public int strikeSize = 5;
    private float strikeTimer = 0f;
    private int strike = 0;
    public float strikeDelay = 0.5f;

    void Start()
    {
        sm = new StateMachine();
        sm.SetState(new StateIdle(this));
        if (flash) {
            flashComponent = flash.GetComponent<Flash>();
        }

        strike = strikeSize;
        flashTimer = flashDelay;  
        strikeTimer = strikeDelay; 
    }

    void Update()
    {
        sm.Update();

        flashTimer += Time.deltaTime;
        if (strike == 0)
        {
            strikeTimer += Time.deltaTime;
            if (strikeTimer >= strikeDelay)
            {
                Recharge();
            }
        }
    }

    public void CheckHealth()
    {
        if (health < 1)
        {
            Debug.Log("Berserker died");
            sm.SetState(new StateDestroying(this));
        }
    }

    private void PrepareStrike(GameObject car)
    {
        if (playerEnter)
        {
            flashedDone = false;
            Debug.Log("CoilTower detected player");
            Transform other = car.transform.Find("Body");
            playerTransform = other;

            float offset = 15f;
                        
            Vector3 r = other.transform.forward * Random.Range(offset, offset*2);
            Debug.Log(r.ToString());
            r = r + other.transform.right * Random.Range(-offset, offset);
            Vector3 rP = r + other.transform.position;            
            Vector3 flashSpawn = rP; //new Vector3 (randomX, other.transform.position.y, maxZ);

            Vector3 world = new Vector3 (0f, -1f, 0f);
            RaycastHit hitGround;
            if (Physics.Raycast((flashSpawn + new Vector3(0f, 100f, 0f)),
                world,
                out hitGround,
                150f,
                layerMask))
            {
                if (hitGround.collider) {
                    flashSpawn.y = hitGround.point.y;
                    //Debug.Log("ground y : " + groundY);
                    spawnLocation = flashSpawn;

                    Charge();
                    //flashedDone = true;
                }
            }
            
        }
    }

    //when bullet hit tower
    private void OnTriggerEnter(Collider collision) {
        Debug.Log("Coil collision");
        if (collision.gameObject.tag == "Player-Bullet") {
            Debug.Log("Coil being shot");
            Damage(1);
        }
    }

    public override void HandleDetection (GameObject other, GameObject detector)
    {
        Debug.Log("Coil : EnemyBody :: HandleDetection()");
        if (other.tag == "Player")
        {
            Debug.Log("Coil detected player");
            playerEnter = true;
            //PrepareStrike(GameObject.Find("Car"));
            return;
        }
    }

    public override void HandleExitDetection (GameObject other, GameObject detector)
    {
        if (other.tag == "Player")
        {
            playerEnter = false;
            return;
        }
        
    }

    void Charge() {
        if (spawnLocation != null) {
            FlashHandle(spawnLocation);         
        }
    }

    public void FlashHandle (Vector3 spawnLocation) {
        //Debug.Log("Flashing ... " + strike + " ... " + flashTimer + " ... " + flashDelay  );
        if (strike == 0)
        {
            return;
        }
        if (flashTimer >= flashDelay)
        {
            if (flash)
            {
                GameObject currentFlash = Instantiate(flash, spawnLocation , Quaternion.identity);
                Destroy(currentFlash, 1);
            }

            flashTimer = 0f;
            strike--;
            if (strike == 0)
            {
                strikeTimer = 0f;
            }
        }
        
    }

    private void Recharge()
    {
        strike = strikeSize;
        strikeTimer = strikeDelay;
    }

    public class StateIdle : IState
    {
        private CoilTower owner;

        public StateIdle (CoilTower o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("Coil enter state idle");
        }
        void IState.Update()
        {
            if (owner.playerEnter)
            {
                owner.sm.SetState(new StateCharging(owner));
            }
            owner.CheckHealth();
        }
        void IState.Exit() {}
    }

    public class StateCharging : IState
    {
        private CoilTower owner;

        public StateCharging (CoilTower o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("Coil enter state charging");
        }

        void IState.Update()
        {
            owner.PrepareStrike(GameObject.Find("Car"));
            owner.CheckHealth();
        }

        void IState.Exit()
        {
        }
    }

    public class StateDestroying : IState
    {
        private CoilTower owner;

        public StateDestroying (CoilTower o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("Coil enter state destroying");
            owner.SelfDestruct(1f);
        }

        void IState.Update()
        {
        }

        void IState.Exit()
        {
        }
    }


}

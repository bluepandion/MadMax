using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : EnemyBody
{
    private CharacterController bc;
    private Transform playerTransform;
    private Vector3 velocity;
    private bool playerEnter = false;
    private StateMachine sm;

    public float moveSpeed = 10f;
    public float friction = 0.9f;
    public float gravity = 9.87f;

    void Start()
    {
        bc = GetComponent<CharacterController>();
        sm = new StateMachine();
        sm.SetState(new StateIdle(this));
    }

    void Update()
    {
        sm.Update();
    }

    void Move()
    {
        Vector3 v = (playerTransform.position - transform.position).normalized * moveSpeed;
        v.y = 0f;
        if (!bc.isGrounded)
        {
            velocity.x *= friction;
            velocity.z *= friction;
            velocity.y -= gravity * Time.deltaTime;
        } else {
            velocity.x = v.x;
            velocity.z = v.z;
            velocity.y = 0f;
        }
        bc.Move(velocity * Time.deltaTime);
    }

    public void CheckHealth()
    {
        if (health < 1)
        {
            Debug.Log("Berserker died");
            sm.SetState(new StateDestroying(this));
        }
    }

    void OnControllerColliderHit (ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            Damage(100);
        }
    }

    void OnTriggerEnter(Collider hit) {
        if (
            hit.gameObject.GetComponent<CarCharacterController>() ||
            hit.gameObject.GetComponent<Lava>() ||
            hit.gameObject.GetComponent<EnemyTower>() ||
            (hit.gameObject.tag == "Player-Bullet")
            )
        {
            Damage(1);
        }
    }

    public override void HandleDetection (GameObject other, GameObject detector)
    {
        Debug.Log("Berserker : EnemyBody :: HandleDetection()");
        if (other.tag == "Player")
        {
            Debug.Log("Beserker detected player");
            playerTransform = other.transform;
            playerEnter = true;
            return;
        }
    }

    public class StateIdle : IState
    {
        private Berserker owner;

        public StateIdle (Berserker o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("Berserker enter state idle");
            owner.velocity = new Vector3(0f, 0f, 0f);
        }
        void IState.Update()
        {
            if (owner.playerEnter)
            {
                owner.sm.SetState(new StateChasing(owner));
            }
            owner.CheckHealth();
        }
        void IState.Exit() {}
    }

    public class StateChasing : IState
    {
        private Berserker owner;

        public StateChasing (Berserker o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("Berserker enter state chasing");
        }

        void IState.Update()
        {
            owner.Move();
            owner.CheckHealth();
        }

        void IState.Exit()
        {
        }
    }

    public class StateDestroying : IState
    {
        private Berserker owner;

        public StateDestroying (Berserker o) { owner = o; }

        void IState.Enter()
        {
            Debug.Log("Berserker enter state destroying");
            owner.SelfDestruct(0f);
            owner.gameObject.GetComponent<CharacterController>().enabled = false;
        }

        void IState.Update()
        {
        }

        void IState.Exit()
        {
        }
    }
}

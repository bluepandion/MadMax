using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject menu;
    public CarCharacterController car;
    public PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        car = car.GetComponent<CarCharacterController>();
        state = new StateDriving(this);
    }

    void Update()
    {
        if (state != null) {
            state.Update();
        }
    }

    public class PlayerState
    {
        protected Player o;
        protected String name = "";
        protected PlayerState previous;

        public PlayerState(Player owner) {
            previous = owner.state;
            o = owner;
        }

        public virtual void Enter() {}

        public virtual void Update() {}

        public virtual void Exit() {}

        public void Return() {
            Exit();
            o.state = previous;
            previous.Enter();
        }

        public void Change(PlayerState to) {
            Exit();
            o.state = to;
            o.state.Enter();
        }
    }

    public class StateDriving : PlayerState
    {
        public StateDriving (Player owner) : base (owner) {}

        public override void Enter() {
            Debug.Log("Player state Driving");
        }

        public override void Update() {
            if (Input.GetButtonDown("Fire1"))
            {
                o.car.Shoot();
            }

            o.car.HandlePhysics(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));

            if (Input.GetButtonDown("Cancel")) {
                Change(new Player.StatePaused(o));
            }
        }
    }

    public class StateMelting : PlayerState
    {
        private float time = 0f;
        private const float MELT_DURATION = 1.0f;

        public StateMelting (Player owner) : base (owner) {}

        public override void Enter() {
            Debug.Log("Player state Melting");
        }

        public override void Update() {
            o.car.Melt();
            time += Time.deltaTime;
            if (time > MELT_DURATION) {
                Change(new Player.StateDead(o));
            }
        }
    }

    public class StateDead : PlayerState
    {
        public StateDead (Player owner) : base (owner) {}

        public override void Enter() {
            Debug.Log("Player state Dead");
        }
    }

    public class StatePaused : PlayerState
    {
        public StatePaused (Player owner) : base (owner) {}

        public override void Enter() {
            Debug.Log("Player state Paused");
            Time.timeScale = 0f;
            o.menu.SetActive(true);
        }

        public override void Update() {
            if (Input.GetButtonDown("Cancel")) {
                Return();
            }
            if (!o.menu.activeInHierarchy) {
                Return();
            }
        }

        public override void Exit() {
            Debug.Log("Player state Paused - Exit");
            o.menu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

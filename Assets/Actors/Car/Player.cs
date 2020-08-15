using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MenuContainer menu;
    public CarCharacterController car;
    private StateMachine sm = new StateMachine();

    private bool colliding = true;

    void Start()
    {
        car = car.GetComponent<CarCharacterController>();
        sm.SetState(new StateDriving(this));
    }

    void Update()
    {
        sm.Update();
    }

    public void TriggerEnter(GameObject other)
    {
        if (!colliding) { return; }

        if (other.tag == "Lava")
        {
            sm.SetState(new StateMelting(this));
        }
        if (other.tag == "Pickup")
        {
            Pickup p = other.GetComponent<Pickup>();
            p.Pick();
            if (p.name == "Star")
            {
                GameState.Instance.player.AddScore(100);
                GameState.Instance.player.AddStars(1);
                if (GameState.Instance.player.stars == GameState.Instance.totalStar)
                {
                    sm.SetState(new StateWin(this));
                }
            }
        }
        if (other.tag == "Enemy-Bullet") {
            sm.SetState(new StateDead(this));
        }
    }

    public void CollisionEnter(GameObject other)
    {

    }

    public class StateIdle : IState
    {
        private Player owner;

        public StateIdle (Player o) { owner = o; }

        void IState.Enter()
        {
        }
        void IState.Update()
        {
        }
        void IState.Exit()
        {
        }
    }

    public class StateDriving : IState
    {
        private Player owner;

        public StateDriving (Player o) { owner = o; }

        void IState.Enter()
        {
        }
        void IState.Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                owner.car.Shoot();
            }

            owner.car.HandlePhysics(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));

            if (Input.GetButtonDown("Cancel"))
            {
                owner.sm.SetState(new StatePaused(owner));
            }
        }
        void IState.Exit()
        {
        }
    }

    public class StateMelting : IState
    {
        private Player owner;
        private float time = 0f;
        private const float MELT_DURATION = 1.0f;

        public StateMelting (Player o) { owner = o; }

        void IState.Enter()
        {
        }
        void IState.Update()
        {
            owner.car.Melt();
            time += Time.deltaTime;
            if (time > MELT_DURATION)
            {
                owner.sm.SetState(new StateDead(owner));
            }
        }
        void IState.Exit()
        {
        }
    }

    public class StateDead : IState
    {
        private Player owner;
        private float time = 0f;
        private const float EXPLODE_DURATION = 2.0f;

        public StateDead (Player o) { owner = o; }

        void IState.Enter()
        {
            owner.car.SelfDestruct(EXPLODE_DURATION - 1f);
        }
        void IState.Update()
        {
            time += Time.deltaTime;
            if (time > EXPLODE_DURATION)
            {
                if (owner.car == null) {
                owner.menu.ShowPage("Page Dead");
                }
            }
        }
        void IState.Exit()
        {
        }
    }

    public class StatePaused : IState
    {
        private Player owner;

        public StatePaused (Player o) { owner = o; }

        void IState.Enter()
        {
            Time.timeScale = 0f;
            owner.menu.ShowPage("Page Pause");
        }
        void IState.Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                owner.menu.Hide();
                owner.sm.SetState(new StateDriving(owner));
            }
            if (!owner.menu.gameObject.activeInHierarchy)
            {
                owner.sm.SetState(new StateDriving(owner));
            }
        }
        void IState.Exit()
        {
            Time.timeScale = 1f;
        }
    }

    public class StateWin : IState
    {
        private Player owner;

        public StateWin (Player o) { owner = o; }

        void IState.Enter()
        {
            Time.timeScale = 0f;
            owner.menu.ShowPage("Page Win");
        }
        void IState.Update()
        {
        }
        void IState.Exit()
        {
            Time.timeScale = 1f;
        }
    }
}

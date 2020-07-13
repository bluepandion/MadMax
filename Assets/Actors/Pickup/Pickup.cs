using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float rotationSpeed = 360.0f;

    public Transform model;
    private Vector3 modelPosition;

    private StateMachine stateMachine = new StateMachine();

    void Start()
    {
        modelPosition = model.position;
        stateMachine.SetState(new StateIdle(this));
    }

    void Update()
    {
        if (model)
        {
            model.rotation = Quaternion.Euler(
                0f,
                Mathf.Repeat(Time.time * rotationSpeed, 360),
                0f
            );
        }
        stateMachine.Update();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Debug.Log("Pickup touched");
        }
    }

    public void Pick()
    {
        SphereCollider c = GetComponent<SphereCollider>();
        if (c)
        {
            c.enabled = false;
        }
        stateMachine.SetState(new StateDisappearing(this));
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public class StateIdle : IState
    {
        private Pickup owner;

        public StateIdle (Pickup o) { owner = o; }

        private const float floatSpeed = 5.0f;
        private Vector3 floatPosition = new Vector3(0f, 1f, 0.0f);

        void IState.Enter()
        {
            Debug.Log("Pickup enter state idle");
        }

        void IState.Update()
        {
            if (owner.model)
            {
                owner.model.position = owner.modelPosition +
                floatPosition * Mathf.Sin(Time.time * floatSpeed);
            }
        }

        void IState.Exit()
        {
        }
    }

    public class StateDisappearing : IState
    {
        private Pickup owner;

        public StateDisappearing (Pickup o) { owner = o; }

        private const float DISAPPEAR_DURATION = 1.0f;
        private const float DISAPPEAR_JUMP = 5.0f;
        private float disappear = 0f;

        void IState.Enter()
        {
            Debug.Log("Pickup enter state disappearing");
        }

        void IState.Update()
        {
            owner.transform.localPosition += new Vector3(
                0f,
                disappear * DISAPPEAR_JUMP,
                0f
            );

            disappear += Time.deltaTime;
            if (disappear >= DISAPPEAR_DURATION)
            {
                owner.Kill();
            }
        }

        void IState.Exit()
        {
        }
    }
}

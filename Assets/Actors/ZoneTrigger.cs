using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Delete this
//
public class ZoneTrigger : MonoBehaviour
{
    public GameObject handler;


    void Start()
    {

    }

    void Update()
    {

    }

    public void HandleZoneEnter(Transform zone) {
        if (handler) {
            handler.SendMessage("blaa");
            Debug.Log("blaa");
        }
    }
}

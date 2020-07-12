using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuPage : MonoBehaviour
{
    private GameObject previousPage;
    public bool allowMenuExitOnBack;

    void Start()
    {

    }

    void Update()
    {

    }

    public virtual void Enter()
    {
        Debug.Log("MenuPage " + gameObject.name + " Enter()");
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform t = transform.parent.GetChild(i);
            if (t.gameObject.activeSelf)
            {
                previousPage = t.gameObject;
            }
        }
        if (previousPage)
        {
            previousPage.SetActive(false);
        }
        gameObject.SetActive(true);
    }

    public virtual void Back()
    {
        Debug.Log("Menu Page - Back()");
        if (previousPage)
        {
            previousPage.SetActive(true);
            gameObject.SetActive(false);
            previousPage = null;
        } else {
            if (allowMenuExitOnBack)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public virtual void Exit()
    {
        previousPage = null;
        transform.parent.gameObject.SetActive(false);
    }

    public virtual void HandleSelection(Transform selection) {

    }
}

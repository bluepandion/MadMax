using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class MenuContainer : MonoBehaviour
{
    public void Start()
    {
    }

    public void Update ()
    {
    }

    public void ShowPage(string pageName)
    {
        Debug.Log("MenuContainer - Show Page " + pageName);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Transform t = transform.Find(pageName);
        if (t)
        {
            MenuPage p = t.GetComponent<MenuPage>();
            if (p)
            {
                p.Enter();
                Show();
            } else {
                Debug.Log(" - Page object did not have a MenuPage component");
            }
        } else {
            Debug.Log(" - Menu page not found.");
        }
    }

    public void Hide()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.parent.gameObject.SetActive(true);
    }
}

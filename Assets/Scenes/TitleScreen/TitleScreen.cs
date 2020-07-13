using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public MenuContainer menu;

    void Start()
    {
        if (menu)
        {
            menu.ShowPage("Page Main");
        }
    }

    void Update()
    {
    }
}

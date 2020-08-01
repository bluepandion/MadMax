using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPageSelection : MenuPage
{
    // Start is called before the first frame update
    public GameObject mainPage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSelectionPage() {
        Enter();
        mainPage.SetActive(false);
        gameObject.SetActive(true);
    }
}

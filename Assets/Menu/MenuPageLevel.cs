using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPageLevel : MenuPage
{
    public GameObject mainPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevelPage() {
        Enter();
        mainPage.SetActive(false);
        gameObject.SetActive(true);
    }
}

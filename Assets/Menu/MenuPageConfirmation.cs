using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPageConfirmation : MenuPage
{
    public GameObject mainPage;
    public GameObject pausePage;
    public GameObject confirmationPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToConfirmPage() {
        Enter();
        pausePage.SetActive(false);
        confirmationPage.SetActive(true);
    }

    public override void GoToMainPage() {
        confirmationPage.SetActive(false);
        mainPage.SetActive(true);
    }
}

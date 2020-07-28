using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuPageLevel : MenuPage
{
    public GameObject mainPage;
    public GameObject mapPage;
    public LevelList levelList;
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
    
    public void GoToMapPage(int num) {
        Enter();
        Debug.Log("Level number: " + num);
        gameObject.SetActive(false);
        mapPage.SetActive(true);
        SceneManager.LoadScene(levelList.levels[num - 1], LoadSceneMode.Single);
    }
}

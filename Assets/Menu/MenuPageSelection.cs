using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPageSelection : MenuPage
{
    // Start is called before the first frame update
    public GameObject mainPage;
    public GameObject playPage;
    public Transform content;
    public LevelList levelList;
    public GameObject lvlButton;
    public GameObject mapImage;
    private Vector3 buttonPos = new Vector3 (0f, 0f, 0f);
    private float yCoordination = -50f; //458f; 
    public string levelNameSelected;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelList.levels.Length; i++ ) {
            GameObject newButton = Instantiate(lvlButton, buttonPos, Quaternion.identity);
            newButton.transform.SetParent(content);
            if (newButton) {
                float newY = yCoordination - 108*i;
                newButton.GetComponent<LevelButtonScript>().SetButton(transform , levelList.levels[i], i, newY);
            }
        }
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GoToScence() {
        Debug.Log("Go to Scence " + levelNameSelected) ;
        Enter();
        SceneManager.LoadScene(levelNameSelected, LoadSceneMode.Single);
        gameObject.SetActive(false);
    }

    public void Scroll(float value)
    {
        Debug.Log("Scrolled " + value.ToString());
    }

    public void GoToSelectionPage() {
        Enter();
        mainPage.SetActive(false);
        gameObject.SetActive(true);
    }
}

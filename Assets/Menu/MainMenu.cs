using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    //private GameObject previousPage;
    public GameObject Menu;
    public GameObject Pause;
    public string previousPage;
    // Start is called before the first frame update
    public void Start() {
        //OptionsTrack();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void OptionsTrack()
    {
         for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            //Debug.Log("all children: " + gameObject.transform.GetChild(i).name);
            if(gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                    string ActiveGameObject = gameObject.transform.GetChild(i).name;
                    Debug.Log("active: " + ActiveGameObject);

                    if(ActiveGameObject == "PauseMenu" | ActiveGameObject == "Menu"){
                        GameObject.Find(ActiveGameObject).SetActive(false);
                        previousPage = ActiveGameObject;
                    }
            }
        }
        /* string  previousPage = transform.GetChild(1).name;
        Debug.Log("active: " + previousPage); */
    }
    public void BackNavigation()
    {
        Transform p;
        Debug.Log("prev page: " + previousPage);
        if (previousPage == "")
        {
            return;
        } else {
            p = transform.Find(previousPage);            
        }
        
        if (p) {
            p.gameObject.SetActive(true);            
        } else {            
            Debug.Log("Previous page object not found");
        }
        previousPage = "";
    }

/*     public void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
        SceneManager.LoadScene (0);
        }
    } */
}

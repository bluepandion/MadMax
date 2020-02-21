using UnityEngine;
using System.Collections;
using TMPro;

public class PageController : MonoBehaviour {
    //public Transform canvas;
    public GameObject menuObj;

    public bool menuActive = true;


    void Start(){
        menuObj = GameObject.Find("Menu");
        Debug.Log("Script PageController Start");
        if(menuObj == null)
            Debug.LogError("MenuObj has not been assigned.", this);
    }

     // Update is called once per frame
     public void Update () {
         //Debug.Log("Update Called");
         if (Input.GetKeyDown(KeyCode.Escape))
         {
             Pause();
         }
     }
     public void Pause()
     {
         Debug.Log("Pause Called");
         menuActive = !menuActive;
         //temporary hide the menu
         //canvas.gameObject.GetComponent<Canvas>().enabled = !menuActive;
         if (menuObj.activeInHierarchy == false)
         {
             menuObj.SetActive(true);
             Time.timeScale = 0;
         }
         else
         {
             menuObj.SetActive(false);
             Time.timeScale = 1;       
         }
    }

}
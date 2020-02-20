using UnityEngine;
using System.Collections;
using TMPro;

public class MouseHover : MonoBehaviour {
    public TextMeshProUGUI textCorlor;
    public Transform canvas;

    public bool menuActive = true;


    void Start(){
	    textCorlor = GetComponent<TextMeshProUGUI>();
        Debug.Log("Script Start");
        if(canvas == null)
            Debug.LogError("Variable has not been assigned.", this);
    }

     // Update is called once per frame
     public void Update () {
         Debug.Log("Update Called");
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
         canvas.gameObject.GetComponent<Canvas>().enabled = !menuActive;
        //  if (canvas.gameObject.activeInHierarchy == false)
        //  {
        //      canvas.gameObject.SetActive(true);
        //      Time.timeScale = 0;
        //  }
        //  else
        //  {
        //      canvas.gameObject.SetActive(false);
        //      Time.timeScale = 1;       
        //  }
    }

    public void OnMouseOver() {
        Debug.Log("Mouse Enter");
        GetComponent<TextMeshProUGUI>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        
    }

    public void OnMouseExit() 
    {
        GetComponent<TextMeshProUGUI>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        Debug.Log("Mouse Exit");
    }

    public void OnMouseDown()
    {
        Debug.Log("Mouse Click");
        gameObject.SetActive(false);
    }

}

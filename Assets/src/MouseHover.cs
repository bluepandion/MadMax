using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour {
    public TextMeshProUGUI textCorlor;
    //public Transform canvas;

    public bool menuActive = true;


    void Start(){
	    textCorlor = GetComponent<TextMeshProUGUI>();
        Debug.Log("Mouse Script Start");
        // if(canvas == null)
        //     Debug.LogError("Variable has not been assigned.", this);
    }

     // Update is called once per frame
     public void Update () {
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

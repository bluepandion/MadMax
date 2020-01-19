using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class MouseHover : MonoBehaviour {
    public TextMeshProUGUI textCorlor;
    void Start(){
	    textCorlor = GetComponent<TextMeshProUGUI>();
        Debug.Log("Mouse Start");
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

}

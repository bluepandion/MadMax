using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    int MenuActiveCount = 0;
    int OptionsActiveCount = 0;
    int PauseActiveCount = 0;
    string[] activeObjectNames = new string[5];
    int keyTrack = 1;
    string nameSelected = ""; 
    Transform transformOjb;
    GameObject GObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.activeSelf)
        {
            MenuActiveCount++;
            if(MenuActiveCount == 1){Debug.Log("Menu Active");}
            MenuActiveCount = -1;
        }else{MenuActiveCount = 0;};
        if (OptionsMenu.activeSelf)
        {
            OptionsActiveCount++;
            if(OptionsActiveCount == 1){Debug.Log("Options Active");activeObjectNames = ObjectTrack(OptionsMenu);}
            OptionsActiveCount = -1;
        }else{OptionsActiveCount = 0;};
        if (PauseMenu.activeSelf)
        {
            PauseActiveCount++;
            if(PauseActiveCount == 1){Debug.Log("Pause Active");activeObjectNames = ObjectTrack(PauseMenu);}
            PauseActiveCount = -1;         
        }else{PauseActiveCount = 0;};
        Debug.Log(activeObjectNames[1]);

        if (Input.GetKeyDown("up"))
        {
            keyTrack--;
            if(keyTrack < 1)keyTrack=1;
            nameSelected = activeObjectNames[keyTrack];
            Debug.Log("up pressed! " + nameSelected);
        }
        if (Input.GetKeyDown("down"))
        {
            keyTrack++;
            if(keyTrack > 3)keyTrack=3;
            nameSelected = activeObjectNames[keyTrack];
            Debug.Log("down pressed! " + nameSelected);
            var GObject  = GameObject.Find(nameSelected);
            Debug.Log("target " + GObject); 
            GObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 128, 0, 255);    
        }
        
    }

    public string[] ObjectTrack(GameObject Obj)
    {
        string[] childrenObjects = new string[5];
        for (int i = 0; i < Obj.transform.childCount; i++)
        {
            //Debug.Log("all children: " + gameObject.transform.GetChild(i).name);
            if(Obj.transform.GetChild(i).gameObject.activeSelf == true)
            {
                childrenObjects[i] = Obj.transform.GetChild(i).name;
                //Debug.Log(childrenObjects[i]);
            }
        }
        return childrenObjects;
        // OptionsMenuObj.SetActive(true);
        /* string  previousPage = transform.GetChild(1).name;
        Debug.Log("active: " + previousPage); */
    }
}

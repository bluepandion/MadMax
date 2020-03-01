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
    string nameSelected = "";
    int keyTrack = 1;
    Transform transformOjb;
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
            if(MenuActiveCount == 1){Debug.Log("Menu Active");activeObjectNames = ObjectTrack(Menu);keyTrack=1;}
            MenuActiveCount = -1;
        }else{MenuActiveCount = 0;};
        if (OptionsMenu.activeSelf)
        {
            OptionsActiveCount++;
            if(OptionsActiveCount == 1){Debug.Log("Options Active");activeObjectNames = ObjectTrack(OptionsMenu);keyTrack=2;}
            OptionsActiveCount = -1;
        }else{OptionsActiveCount = 0;};
        if (PauseMenu.activeSelf)
        {
            PauseActiveCount++;
            if(PauseActiveCount == 1){Debug.Log("Pause Active");activeObjectNames = ObjectTrack(PauseMenu);keyTrack=1;}
            PauseActiveCount = -1;         
        }else{PauseActiveCount = 0;};
        //Debug.Log(activeObjectNames[1]);

        //keyTrack = (OptionsActiveCount == 1)? 2: 1;
        if (Input.GetKeyDown("up"))
        {
            keyTrack--;
            if(keyTrack < 1)keyTrack=1;
            UpDownPressed(keyTrack, false);
        }
        if (Input.GetKeyDown("down"))
        {
            keyTrack++;
            if(keyTrack > 3)keyTrack=3;
            UpDownPressed(keyTrack, true);       
        }
        if (Input.GetKeyDown("left"))
        {
            if( nameSelected == "VolumeText")LeftRightPressed("left");                 
        }
        if (Input.GetKeyDown("right"))
        {
           if( nameSelected == "VolumeText")LeftRightPressed("right");
                  
        }
        if (Input.GetKeyDown("return"))
        {
           EnterPressed();        
        }
    }

    public string[] ObjectTrack(GameObject Obj)
    {
        string[] childrenObjects = new string[5];
        int index = (Obj.name == "OptionsMenu")? 2: 1;
        for (int i = index; i < Obj.transform.childCount; i++)
        {
            //Debug.Log("all children: " + gameObject.transform.GetChild(i).name);
            if(Obj.transform.GetChild(i).gameObject.activeSelf == true)
            {
                childrenObjects[i] = Obj.transform.GetChild(i).name;
                Debug.Log(childrenObjects[i]);
            }
        }
        return childrenObjects;
        // OptionsMenuObj.SetActive(true);
        /* string  previousPage = transform.GetChild(1).name;
        Debug.Log("active: " + previousPage); */
    }

    public void UpDownPressed(int keyTrack, bool down)
    {
        nameSelected = activeObjectNames[keyTrack];
        string prevNameSelected = "";
        prevNameSelected = (down == true) ?  activeObjectNames[keyTrack -1] : activeObjectNames[keyTrack +1];  
        Debug.Log("down pressed! " + nameSelected + keyTrack);
        var GObject  = GameObject.Find(nameSelected);
        var prevGObject  = GameObject.Find(prevNameSelected);
        Debug.Log("target " + GObject);
        if(prevGObject){prevGObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);}; 
        GObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 128, 0, 255);  

    }

    public void EnterPressed()
    {
        Debug.Log("ENTER");
        var EnterObject  = GameObject.Find(nameSelected);
        if(nameSelected != "VolumeText")EnterObject.GetComponent<Button>().onClick.Invoke();
    }

    public void LeftRightPressed(string direction)
    {
        Slider SlideObject  = GameObject.Find("Slider").GetComponent<Slider>();
        Debug.Log("Left Right " + SlideObject);
        if(direction == "right")SlideObject.value += 0.05f;
        if(direction == "left")SlideObject.value -= 0.05f;

    }
}

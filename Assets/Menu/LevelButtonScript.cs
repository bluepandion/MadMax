using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonScript : MenuPage
{
    // Start is called before the first frame update
    public MenuPageSelection selectionMenu;
    //private LevelList lvlList;
    
    void Start()
    {
        //Transform p = transform.parent;       
        //selectionMenu = p.transform.parent.GetComponent<MenuPageSelection>();
        //lvlList = selectionMenu.levelList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButton(Transform selection, string str, int index, float newY){
        selectionMenu = selection.GetComponent<MenuPageSelection>();
        TextMeshProUGUI txt = transform.GetComponent<TextMeshProUGUI>();
        if (txt) {
            gameObject.name = str;
            txt.text = (index+1) + " - " + str;
            txt.GetComponent<RectTransform>().localPosition = new Vector3 (0f, newY, 0f) ;
        }
        Debug.Log("create lvl button " + newY);
    }

    public void GoToScence() {
        Enter();
        selectionMenu.gameObject.SetActive(false);
        selectionMenu.playPage.SetActive(true);
        SceneManager.LoadScene(gameObject.name, LoadSceneMode.Single);
    }

    public void SelectLevel() {
        selectionMenu.levelNameSelected = gameObject.name;
        transform.GetComponent<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
        SetImage();
    }

    public void SetImage() {
        GameObject img = selectionMenu.mapImage;
        img.SetActive(true);
        Texture2D texture = Resources.Load ("Images/" + selectionMenu.levelNameSelected) as Texture2D;
        img.GetComponent<RawImage> ().texture = texture;
    }
}

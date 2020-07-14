using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class MenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 cachedScale;
    private Color32 cachedColor;
    public GameObject targetPage;

    //private bool hover = false;

    void Start()
    {
        cachedScale = transform.localScale;
        cachedColor = GetComponent<TextMeshProUGUI>().color;
    }

    void Update()
    {

    }

    public void Reset()
    {
        transform.localScale = cachedScale;
        GetComponent<TextMeshProUGUI>().color = cachedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = cachedScale;
        GetComponent<TextMeshProUGUI>().color = cachedColor;
    }
}

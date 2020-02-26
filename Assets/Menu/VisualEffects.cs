using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class VisualEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 cachedScale;
    // Start is called before the first frame update
    void Start()
    {
        cachedScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
 
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
 
    public void OnPointerExit(PointerEventData eventData) 
    {
         transform.localScale = cachedScale;
    }

}

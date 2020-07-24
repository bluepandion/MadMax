using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    // Start is called before the first frame update
    private int layerMask = ~(1 << 9);
    public GameObject thunder;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        ThunderHandler();       
    }

    void ThunderHandler() {
        GameObject currentFlash = Instantiate(thunder, transform.position , Quaternion.identity);
        Destroy(currentFlash, 1);            
    }
}

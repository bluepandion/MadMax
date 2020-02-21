using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring
{    
    const float start = 0.5f;

    public float value = 0.0f;

    private float k = 12.0f;
    private float x = start;      
    private float energy = 0.5f * 2.0f * (start * start);  
  
    
    public float Update(float delta, float affectingForce)
    {
        
        float e = 0.5f * k * (x * x) * delta;        
        x += Mathf.Sqrt(Mathf.Abs((2.0f * e) / k));
                
        energy -= e;        

        if (x != 0f) {
            Debug.Log(Mathf.Floor(x * 10000.0f));
        }
        
        value = x;
        return value;
    }
}

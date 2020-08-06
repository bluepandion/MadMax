using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class Level : MonoBehaviour
{
    private Vector3 center = new Vector3(0f, 0f, 0f);
    private int starsNum = 0; 

    void Start()
    {
        if (Application.IsPlaying(gameObject)) {
            CountStars();
        }
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject)) {
            transform.localPosition = center;
        }
    }

    void CountStars() {
        GameObject[] star = GameObject.FindGameObjectsWithTag("Star");
        GameState.Instance.totalStar = star.Length;
        Debug.Log("Total Stars: " + GameState.Instance.totalStar);

    }
}

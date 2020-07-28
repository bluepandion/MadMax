using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelList : ScriptableObject
{
    public string[] levels;

    void Awake()
    {
        Debug.Log("LevelList Awake()");
    }
}

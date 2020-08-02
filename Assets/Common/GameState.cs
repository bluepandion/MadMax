using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

 public class GameState : MonoBehaviour
 {
     private static GameState instance = null;
     public PlayerState player = new PlayerState();

     // Game Instance Singleton
     public static GameState Instance
     {
         get
         {
             if (instance == null)
             {
                GameObject g = new GameObject();
                g.name = "GameState Singleton";
                instance = g.AddComponent(typeof(GameState)) as GameState;
                DontDestroyOnLoad( g );
             }
             return instance;
         }
     }

     private void Awake()
     {
         // if the singleton hasn't been initialized yet
         if (instance != null && instance != this)
         {
             Destroy(this.gameObject);
         }
     }

     void Update()
     {
         //Debug.Log( "SINGLETON UPDATE" );
     }

     public class PlayerState
     {
         public Dictionary<string, int> levelScores = new Dictionary<string, int>();
         public string name = "Player 1";
         public string level;
         public GameObject car;
         public GameObject gun;
         public int kills = 0;
         public int stars = 0;
         public int score = 0;
         public int nitros = 0;

         public void AddScore(int s)
         {
             score += s;
         }

         public void ResetLevelState()
         {
             score = 0;
             kills = 0;
             stars = 0;
             nitros = 0;
         }
     }
 }
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

 public class GameState : MonoBehaviour
 {
     private static GameState instance = null;
     private static PlayerState player = null;

     // Game Instance Singleton
     public static GameState Instance
     {
         get
         {
            return instance;
         }
     }

     public static PlayerState Player
     {
         get
         {
            if (player == null)
            {
                player = new PlayerState();
                Debug.Log("New PlayerState singleton created via get");
            }
            return player;
         }
     }

     private void Awake()
     {
         if (Application.IsPlaying(gameObject)) {
            // if the singleton hasn't been initialized yet
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            } else {
                Debug.Log("GameState singleton created via Awake");
                instance = this;
                DontDestroyOnLoad( this.gameObject );
            }
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
         public Scene level;
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
             Debug.Log("PlayerState singleton :: ResetLevelState()");
         }
     }
 }
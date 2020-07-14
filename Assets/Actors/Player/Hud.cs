using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

    public Text scoreText;
    private int visibleScore = 0;

    void Start()
    {
        SetScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (visibleScore != GameState.Instance.player.score)
        {
            int diff = GameState.Instance.player.score - visibleScore;
            visibleScore += Mathf.Clamp(diff / 2, 1, 100);
            visibleScore = Mathf.Clamp(visibleScore, 0, GameState.Instance.player.score);
            SetScore(visibleScore);
        }
    }

    public void SetScore(int score)
    {
        Debug.Log("Update score " + score.ToString());
        if (scoreText != null)
        {
            scoreText.text = score.ToString("D8");
        } else {
            Debug.Log("Hud : No score text found");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scoreText == null)
        {
            GameObject hud = GameObject.Find("HUD_ScoreText");
            if (hud != null)
                scoreText = hud.GetComponent<TextMeshProUGUI>();
        }
        UpdateUI();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }

    public int GetScore() => score;

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "SCORE: " + score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}
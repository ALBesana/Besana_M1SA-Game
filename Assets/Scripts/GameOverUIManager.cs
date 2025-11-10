using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    public void Start()
    {
        GameManager.Instance?.UnlockCursor();
    }

    public void OnRestartButtonPressed()
    {
        ScoreManager.instance?.ResetScore();
        SceneManager.LoadScene(GameManager.Instance.level1Scene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(GameManager.Instance.mainMenuScene);

        // Reset game state
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameStarted = false;
            GameManager.Instance.UnlockCursor();
        }
    }
}

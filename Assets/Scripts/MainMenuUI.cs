using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Names")]
    public string level1Scene = "Level1";
    public string mainMenuScene = "MainMenu";

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStartGamePressed()
    {
        ScoreManager.instance?.ResetScore();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            SceneManager.LoadScene(level1Scene);
        }
    }

    public void OnQuitPressed()
    {
        Application.Quit();
        Debug.Log("Game closed.");
    }
}

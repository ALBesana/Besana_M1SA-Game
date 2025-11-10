using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string mainMenuScene = "MainMenu";
    public string level1Scene = "Level1";
    public string level2Scene = "Level2";
    public string gameOverScene = "GameOver";
    public string gameFinishedScene = "GameFinished";

    [Tooltip("Player prefab (or Player GameObject) to spawn")]
    public GameObject playerPrefab;

    public bool gameStarted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == level1Scene || scene.name == level2Scene)
        {
            if (gameStarted)
            {
                SpawnPlayerAtSpawnPoint();
            }
        }
    }

    public void StartGame()
    {
        ScoreManager.instance?.ResetScore();

        gameStarted = true;
        SceneManager.LoadScene(level1Scene);
    }

    public void SpawnPlayerAtSpawnPoint()
    {
        Transform sp = GameObject.Find("SpawnPoint")?.transform;
        if (sp == null)
        {
            Debug.LogWarning("SpawnPoint not found - spawning at Vector3.zero");
            sp = new GameObject("SpawnPoint_Temp").transform;
            sp.position = Vector3.zero;
        }

        if (playerPrefab == null)
        {
            GameObject existing = GameObject.Find("Player");
            if (existing != null)
            {
                existing.transform.position = sp.position;
                existing.transform.rotation = sp.rotation;
            }
            else
            {
                Debug.LogError("No playerPrefab assigned and no Player found in scene.");
            }
            return;
        }

        GameObject player = Instantiate(playerPrefab, sp.position, sp.rotation);
        player.name = "Player";
    }

    public void LoadNextLevelOrFinish()
    {
        Scene active = SceneManager.GetActiveScene();
        if (active.name == level1Scene)
            SceneManager.LoadScene(level2Scene);
        else if (active.name == level2Scene)
            SceneManager.LoadScene(gameFinishedScene);
        else
            SceneManager.LoadScene(gameFinishedScene);
    }

    public void PlayerFellInWater(string reason = "You fell in the water")
    {
        ScoreManager.instance?.ResetScore();

        PlayerPrefs.SetString("GameOverMessage", reason);
        SceneManager.LoadScene(gameOverScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        gameStarted = false;
    }

    public void RestartCurrentLevel()
    {
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game closed.");
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
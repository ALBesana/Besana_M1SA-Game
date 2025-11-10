using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameFinishedUIManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        GameManager.Instance?.UnlockCursor();

        if (finalScoreText != null && ScoreManager.instance != null)
        {
            finalScoreText.text = "FINAL SCORE: " + ScoreManager.instance.GetScore();
        }

        ScoreManager.instance?.ResetScore();
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene(GameManager.Instance.mainMenuScene);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameStarted = false;
            GameManager.Instance.UnlockCursor();
        }
    }
}

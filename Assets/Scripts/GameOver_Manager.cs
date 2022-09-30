using UnityEngine;
using UnityEngine.UI;

public class GameOver_Manager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

    public void SetGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        CoinCollection.totalCoins = 0;
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void SetLevelComplete()
    {
        levelCompleteScreen.SetActive(true);
        Time.timeScale = 0;
    }
}


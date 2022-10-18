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
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void startLevel1()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void startLevel2()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void startLevel3()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void SetLevelComplete()
    {
        levelCompleteScreen.SetActive(true);
        Time.timeScale = 0;
    }
}


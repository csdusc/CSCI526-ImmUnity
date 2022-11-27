using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject puaseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(GamePaused) {
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    public void Resume() {
        puaseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;

    }
    public void Pause() {
        puaseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;

    }

    public void Home() {
        puaseMenuUI.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}

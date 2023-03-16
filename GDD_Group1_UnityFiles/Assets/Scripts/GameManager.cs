//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject levelCompletePanel;

    public bool isOver = false;

    public void GameOver()
    {
        Debug.Log("Game Over ;~;");
        gameOverPanel.SetActive(true);
        isOver = true;
        gameOverText.text = "";
    }
    public void GameOver(string textToShow)
    {
        Debug.Log("Game Over ;~;");
        gameOverPanel.SetActive(true);
        isOver = true;

        gameOverText.text = textToShow;
    }

    public void LevelComplete()
    {
        Debug.Log("Level Complete! :D");
        levelCompletePanel.SetActive(true);
        isOver = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToLevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }
}

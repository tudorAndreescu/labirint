using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject youWonText;
    public GameObject youLostText;

    private void Start()
    {
        if (StaticValues.gameWon)
        {
            youWonText.SetActive(true);
            youLostText.SetActive(false);
        }
        else
        {
            youWonText.SetActive(false);
            youLostText.SetActive(true);
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void GoToMenu()
    {
        StaticValues.difficulty = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

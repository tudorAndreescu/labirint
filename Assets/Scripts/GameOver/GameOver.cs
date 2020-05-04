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
            AudioManager.instance.Play("GameWon");
            youWonText.SetActive(true);
            youLostText.SetActive(false);
        }
        else
        {
            AudioManager.instance.Play("GameLost");
            youWonText.SetActive(false);
            youLostText.SetActive(true);
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        AudioManager.instance.Stop(StaticValues.gameWon ? "GameWon" : "GameLost");
    }


    public void GoToMenu()
    {
        StaticValues.difficulty = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        AudioManager.instance.Stop(StaticValues.gameWon ? "GameWon" : "GameLost");
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        AudioManager.instance.Stop(StaticValues.gameWon ? "GameWon" : "GameLost");
        Application.Quit();
    }
}

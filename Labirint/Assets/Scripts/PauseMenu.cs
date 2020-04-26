using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }


    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void LoadMenu()
    {
        Debug.Log("Loading menu...");

        ///Instead of printing, load menu scene here once it is up and running
        ///use a variable instead of hard coding
        //SceneManager.LoadScene("Menu");
        //Time.timeScale = 1f;
    }


    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }



}

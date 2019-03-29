using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public UIElement pauseMenu;

    private bool isPaused = false;

    void Update() {
        if (Input.GetButtonUp("Cancel")) {
            if (!isPaused) {
                Pause();
            }
        }
    }

    public void Pause() {
        isPaused = true;
        pauseMenu.Show(false);
        Time.timeScale = 0.00001f;
    }

    public void Resume() {
        isPaused = false;
        pauseMenu.Hide(false);
        Time.timeScale = 1.0f;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}

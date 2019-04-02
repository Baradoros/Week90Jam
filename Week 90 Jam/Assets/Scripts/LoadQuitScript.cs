using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadQuitScript : MonoBehaviour
{
    public void loadGame(string LevelName)
    {
        Debug.Log("Loading " + LevelName);
        SceneManager.LoadScene(LevelName);
    }

    public void endGame()
    {
        Application.Quit();
    }
}

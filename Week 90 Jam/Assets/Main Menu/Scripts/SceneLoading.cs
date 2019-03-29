using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour {

    // Streamline scene loading
    private IEnumerator LoadSceneDelayed(string name) {
        float delay = 1.0f;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ReloadLevel() {
        Scene scene = SceneManager.GetActiveScene();
        LoadScene(scene.name);
    }

    public void LoadScene(string name) {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneDelayed(name));
    }

    public void QuitToMenu() {
        Time.timeScale = 1.0f; // In case game was paused when loading
        LoadScene("MainMenu");
    }

    public void QuitToDesktop() {
        Quit();
    }

    void Quit() {

#if UNITY_EDITOR // If we're in Unity Editor, stop play mode
        if (UnityEditor.EditorApplication.isPlaying == true)
            UnityEditor.EditorApplication.isPlaying = false;
#else // If we are in a built application, quit to desktop
            Application.Quit();
#endif
    }
}

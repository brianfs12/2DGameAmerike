using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1.0f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0));
    }

    public void LoadDeathScreen()
    {
        StartCoroutine(LoadLevel(3));
    }

    public void LoadOptions()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void Exit()
    {
        Application.Quit(); 
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}

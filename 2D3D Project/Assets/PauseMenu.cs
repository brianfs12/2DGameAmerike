using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    private void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.isPaused)
        {
            PauseMenuUI.SetActive(true);
        }
        else
        {
            PauseMenuUI.SetActive(false);
        }
    }

    public void Resume()
    {
        GameManager.Resume();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [SerializeField]
    public GameObject PauseUI;
    public static bool gameIsPaused = false;
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    Canvas dungeonCanvas;
    Camera mainCam;

    void Start()
    {
        dungeonCanvas = GameObject.Find("DungeonCanvas").GetComponent<Canvas>();
        mainCam = Camera.main;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadBattle()
    {
        Debug.Log("Loading Battle");
        mainCam.enabled = false;
        dungeonCanvas.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    public void LoadWin()
    {
        SceneManager.UnloadSceneAsync(2);
        mainCam.enabled = true;
        dungeonCanvas.gameObject.SetActive(true);
        Debug.Log("Loading Win");
        // SceneManager.LoadScene(3);
    }

    public void LoadLose()
    {
        Debug.Log("Loading Lose");
        SceneManager.LoadScene(4);
    }
}

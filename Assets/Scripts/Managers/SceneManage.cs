using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void LoadDungeon()
    {
        Debug.Log("Loading Dungeon");
        SceneManager.LoadScene(1);
    }

    public void LoadBattle()
    {
        Debug.Log("Loading Battle");
        SceneManager.LoadScene(2);
    }

    public void LoadWin()
    {
        Debug.Log("Loading Win");
        SceneManager.LoadScene(3);
    }

    public void LoadLose()
    {
        Debug.Log("Loading Lose");
        SceneManager.LoadScene(4);
    }

}

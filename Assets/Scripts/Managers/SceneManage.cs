using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void LoadDungeon()
    {
        Debug.Log("Loading Dungeon");
        SceneManager.LoadScene(2);
    }
    public void LoadTutorial()
    {
        Debug.Log("Loading Tutorial");
        SceneManager.LoadScene(1);
    }

    public void LoadWin()
    {
        Debug.Log("Loading Win");
        SceneManager.LoadScene(5);
    }

    public void LoadLose()
    {
        Debug.Log("Loading Lose");
        SceneManager.LoadScene(5);
    }
}

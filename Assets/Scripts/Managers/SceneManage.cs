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
        SceneManager.LoadScene(1);
    }

}

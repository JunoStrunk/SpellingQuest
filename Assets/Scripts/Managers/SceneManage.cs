using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    ToDungeon,
    BackToDungeon,
    ToBattle,
    ToTutorial,
    ToTown,
    BackToTown,
};

public class SceneManage : MonoBehaviour
{
    [SerializeField]
    float pause = 3f;

    public enum CurrScene
    {
        Main,
        BattleNoDungeon,
        Battle,
        Dungeon
    };

    public CurrScene currScene = new CurrScene();

    Canvas dungeonCanvas;
    Camera mainCam;
    PlayerMovement playerMove;
    delegate void FunctionalLoad();
    FunctionalLoad functionalLoad;

    void Start()
    {
        if (currScene == CurrScene.Main)
            return;
        GeneralEventManager.current.onLoadBattle += BattleLoadHandler;
        GeneralEventManager.current.onLoadDungeon += DungeonLoadHandler;

        if (currScene == CurrScene.BattleNoDungeon)
            return;
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        dungeonCanvas = GameObject.Find("DungeonCanvas").GetComponent<Canvas>();
        mainCam = Camera.main;
        GeneralEventManager.current.onWin += LoadUltWin;
        DontDestroyOnLoad(this);
    }

    void OnDestroy()
    {
        if (currScene == CurrScene.Main)
            return;

        GeneralEventManager.current.onLoadDungeon -= DungeonLoadHandler;
        GeneralEventManager.current.onLoadBattle -= BattleLoadHandler;
    }

    public void BattleLoadHandler(SceneType sceneType)
    {
        if (sceneType == SceneType.ToBattle)
        {
            functionalLoad = LoadToBattle;
        }
        else if (sceneType == SceneType.ToTutorial)
        {
            functionalLoad = LoadTutorial;
        }
        StartCoroutine(PauseBeforeLoad());
    }

    public void DungeonLoadHandler(SceneType sceneType)
    {
        if (sceneType == SceneType.BackToDungeon && currScene != CurrScene.BattleNoDungeon)
        {
            functionalLoad = LoadBackToDungeon;
        }
        else if (sceneType == SceneType.ToDungeon || currScene == CurrScene.BattleNoDungeon)
        {
            Debug.Log("Set Functional Load");
            functionalLoad = LoadDungeon;
        }
        StartCoroutine(PauseBeforeLoad());
    }

    IEnumerator PauseBeforeLoad()
    {
        yield return new WaitForSeconds(pause);
        if (functionalLoad != null)
            functionalLoad();
    }

    public void LoadDungeon()
    {
        Debug.Log("Loading Dungeon");
        SceneManager.LoadScene(2);
    }
    public void LoadTutorial()
    {
        functionalLoad = TutorialHandler;
        StartCoroutine(PauseBeforeLoad());
    }

    void TutorialHandler()
    {
        Debug.Log("Loading Tutorial");
        SceneManager.LoadScene(1);
    }

    public void LoadUltWin()
    {
        functionalLoad = UltWinHandler;
        StartCoroutine(PauseBeforeLoad());
    }
    void UltWinHandler()
    {
        Debug.Log("Loading Win");
        SceneManager.LoadScene(4);
    }

    public void LoadUltLose()
    {
        Debug.Log("Loading Lose");
        SceneManager.LoadScene(5);
    }

    public void LoadToBattle()
    {
        Debug.Log("Loading ToBattle");
        mainCam.enabled = false;
        dungeonCanvas.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

    public void LoadBackToDungeon()
    {
        Debug.Log("Loading BackToDungeon");
        playerMove.CanPlayerMove(true);
        SceneManager.UnloadSceneAsync(3);
        mainCam.enabled = true;
        dungeonCanvas.gameObject.SetActive(true);
    }
}

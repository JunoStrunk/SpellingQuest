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
        Dungeon,
        Town
    };

    public CurrScene currScene = new CurrScene();

    Canvas dungeonCanvas;
    Camera mainCam;
    PlayerMovement playerMove;
    delegate void FunctionalLoad(int id);
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

    public void BattleLoadHandler(SceneType sceneType, int batID)
    {
        if (sceneType == SceneType.ToBattle)
        {
            functionalLoad = LoadToBattle;
        }
        // else if (sceneType == SceneType.ToTutorial)
        // {
        //     functionalLoad = LoadTutorial;
        // }
        StartCoroutine(PauseBeforeLoad(batID));
    }

    public void DungeonLoadHandler(SceneType sceneType, int dungID)
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
        StartCoroutine(PauseBeforeLoad(dungID));
    }

    IEnumerator PauseBeforeLoad(int id)
    {
        yield return new WaitForSeconds(pause);
        if (functionalLoad != null)
            functionalLoad(id);
    }

    public void LoadDungeon(int id)
    {
        Debug.Log("Loading Dungeon");
        SceneManager.LoadScene(id);
    }
    public void LoadTown(int id)
    {
        Debug.Log("Loading Town");
        SceneManager.LoadScene(1);
    }

    // public void LoadTutorial(int id)
    // {
    //     functionalLoad = TutorialHandler;
    //     StartCoroutine(PauseBeforeLoad(0));
    // }

    // void TutorialHandler(int id)
    // {
    //     Debug.Log("Loading Tutorial");
    //     SceneManager.LoadScene(3);
    // }

    public void LoadUltWin()
    {
        functionalLoad = UltWinHandler;
        StartCoroutine(PauseBeforeLoad(0));
    }
    void UltWinHandler(int id)
    {
        Debug.Log("Loading Win");
        SceneManager.LoadScene(5);
    }

    public void LoadUltLose(int id)
    {
        Debug.Log("Loading Lose");
        SceneManager.LoadScene(6);
    }

    public void LoadToBattle(int id)
    {
        Debug.Log("Loading ToBattle");
        mainCam.enabled = false;
        dungeonCanvas.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);
    }

    public void LoadBackToDungeon(int id)
    {
        Debug.Log("Loading BackToDungeon");
        playerMove.CanPlayerMove(true);
        SceneManager.UnloadSceneAsync(id);
        mainCam.enabled = true;
        dungeonCanvas.gameObject.SetActive(true);
    }
}

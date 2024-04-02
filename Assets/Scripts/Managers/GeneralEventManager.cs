using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneralEventManager : MonoBehaviour
{
    //Create singleton for event system :)
    public static GeneralEventManager current;

    void Awake()
    {
        current = this;
        SceneManage.CurrScene currScene = GetComponent<SceneManage>().currScene;
        if (currScene == SceneManage.CurrScene.BattleNoDungeon || currScene == SceneManage.CurrScene.Main)
            return;
        DontDestroyOnLoad(this);
    }

    //========================== Example =======================
    // public event Action<bool> onPlayerHides;
    // public void PlayerHides(bool state)
    // {
    //     if (onPlayerHides != null)
    //         onPlayerHides(state);
    // }
    //=============================-----=======================

    //========================== PlayerTap =======================
    // For dialogue
    public event Action onPlayerTap;
    public void PlayerTap()
    {
        if (onPlayerTap != null)
            onPlayerTap();
    }
    //=============================-----=======================

    //========================== LoadDungeon =======================
    public event Action<SceneType> onLoadDungeon;
    public void LoadDungeon(SceneType sceneType)
    {
        if (onLoadDungeon != null)
        {
            Debug.Log("EventTriggered");
            onLoadDungeon(sceneType);
        }
    }
    //=============================-----=======================

    //========================== LoadDungeon =======================
    public event Action<SceneType> onLoadBattle;
    public void LoadBattle(SceneType sceneType)
    {
        if (onLoadBattle != null)
            onLoadBattle(sceneType);
    }
    //=============================-----=======================

    //========================== LoadDungeon =======================
    public event Action onPlayerDeath;
    public void PlayerDeath()
    {
        if (onPlayerDeath != null)
            onPlayerDeath();
    }
    //=============================-----=======================

    //========================== EnemyDefeat =======================
    public event Action onEnemyDefeat;
    public void EnemyDefeat()
    {
        if (onEnemyDefeat != null)
            onEnemyDefeat();
    }
    //=============================-----=======================

    //========================== ItemCollect =======================
    public event Action<int> onItemCollect;
    public void ItemCollect(int id)
    {
        if (onItemCollect != null)
            onItemCollect(id);
    }
    //=============================-----=======================

    //========================== Win =======================
    public event Action onWin;
    public void Win()
    {
        if (onWin != null)
            onWin();
    }
    //=============================-----=======================

}

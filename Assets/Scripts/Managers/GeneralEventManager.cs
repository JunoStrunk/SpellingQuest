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
        if (currScene == SceneManage.CurrScene.BattleNoDungeon || currScene == SceneManage.CurrScene.Main || currScene == SceneManage.CurrScene.Town)
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
        {
            onPlayerTap();
        }
    }
    //=============================-----=======================

    //========================== LoadDungeon =======================
    public event Action<SceneType, int> onLoadDungeon;
    public void LoadDungeon(SceneType sceneType, int dungID)
    {
        if (onLoadDungeon != null)
        {
            Debug.Log("EventTriggered");
            onLoadDungeon(sceneType, dungID);
        }
    }
    //=============================-----=======================

    //========================== LoadDungeon =======================
    public event Action<SceneType, int> onLoadBattle;
    public void LoadBattle(SceneType sceneType, int batID)
    {
        if (onLoadBattle != null)
            onLoadBattle(sceneType, batID);
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

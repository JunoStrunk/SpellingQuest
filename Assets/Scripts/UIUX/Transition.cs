using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    Animator anim;
    UnityEngine.UI.Image img;
    public bool isDungeon;
    public bool isTutorial = false;

    [SerializeField]
    float pauseBeforeLoad = 2f;

    SceneManage sceneManage;
    BattleSceneManager battleSceneManager;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        img = this.GetComponent<UnityEngine.UI.Image>();
        img.enabled = true;
        if (isDungeon)
        {
            battleSceneManager = GameObject.Find("SceneManager").GetComponent<BattleSceneManager>();
        }
        sceneManage = GameObject.Find("SceneManager").GetComponent<SceneManage>();
    }

    public void LoadDungeonNonEnum()
    {
        if (battleSceneManager)
            anim.SetTrigger("LoadDungeon");
    }

    public IEnumerator LoadDungeon()
    {
        yield return new WaitForSeconds(pauseBeforeLoad);
        if (isTutorial)
            sceneManage.LoadDungeon();
        if (!isDungeon && !isTutorial)
            battleSceneManager.LoadWin();
    }

    public void LoadBattleNonEnum()
    {
        if (battleSceneManager)
            anim.SetTrigger("LoadBattle");
    }

    public IEnumerator LoadBattle()
    {
        yield return new WaitForSeconds(pauseBeforeLoad);
        if (isDungeon)
            battleSceneManager.LoadBattle();
    }
}

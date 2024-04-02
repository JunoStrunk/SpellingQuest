using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    Animator anim;
    UnityEngine.UI.Image img;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        img = this.GetComponent<UnityEngine.UI.Image>();
        img.enabled = true;
        GeneralEventManager.current.onLoadDungeon += SlideIn;
        GeneralEventManager.current.onLoadBattle += SlideIn;
    }

    void OnDestroy()
    {
        GeneralEventManager.current.onLoadDungeon -= SlideIn;
        GeneralEventManager.current.onLoadBattle -= SlideIn;
    }

    public void SlideIn(SceneType sceneType)
    {
        anim.SetTrigger("SlideIn");
    }

    public void SlideInNoArgs()
    {
        anim.SetTrigger("SlideIn");
    }
}

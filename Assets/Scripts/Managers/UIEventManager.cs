using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    //Create singleton for event system :)
    public static UIEventManager current;

    void Awake()
    {
        current = this;
    }

    //========================== Example =======================
    // public event Action<bool> onPlayerHides;
    // public void PlayerHides(bool state)
    // {
    //     if (onPlayerHides != null)
    //         onPlayerHides(state);
    // }
    //=============================-----=======================

    //========================== Add Word to Used Spells =======================
    public event Action<string> onUsedSpell;
    public void UsedSpell(string spell)
    {
        if (onUsedSpell != null)
            onUsedSpell(spell);
    }
    //=============================-----=======================
}
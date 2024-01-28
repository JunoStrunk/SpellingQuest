using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void AttackEvent(int enemyID, Attack attack);
    public event AttackEvent OnAttackEvent;

    public void InvokeAttackEvent(int enemyID, Attack attack)
    {
        if (OnAttackEvent != null)
            OnAttackEvent(enemyID, attack);
    }

    public delegate void ColorEvent(float timing);
    public event ColorEvent OnColorEvent;

    public void InvokeColorEvent(float timing)
    {
        if (OnColorEvent != null)
            OnColorEvent(timing);
    }
}

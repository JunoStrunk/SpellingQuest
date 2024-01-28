using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : IStats
{
    public bool dodging;
    public float dodgeTime;

    public void Start()
    {
        health = GetComponent<Health>();
        dodging = false;
    }

    public override void Damage(float dmg)
    {
        if (!dodging)
        {
            base.Damage(dmg);
        }
    }

    public void Dodge()
    {
        EventManager.Instance.InvokeColorEvent(dodgeTime);
        StartCoroutine(DodgeTiming());
    }

    private IEnumerator DodgeTiming()
    {
        dodging = true;
        yield return new WaitForSeconds(dodgeTime);
        dodging = false;
    }
}

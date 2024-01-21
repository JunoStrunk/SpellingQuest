using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Battle/Attack")]
public class Attack : ScriptableObject
{
    public string attackName;
    public float damage;
    public bool overTime;
    public int timePeriod;

    public void Damage()
    {
        Debug.Log(attackName + " " + damage);
    }
}

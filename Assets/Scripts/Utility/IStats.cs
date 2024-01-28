using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStats : MonoBehaviour
{
    [SerializeField]
    int id;
    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    public int GetID()
    {
        return id;
    }

    public virtual void Damage(float dmg)
    {
        if (health != null)
            health.Damage(dmg);
        else
            Debug.Log("health is null");
    }
    public virtual void Heal(float healing)
    {
        if (health != null)
            health.Heal(healing);
        else
            Debug.Log("health is null");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthUIEvent : UnityEvent<float>
{
}

public class Health : MonoBehaviour
{
    public HealthUIEvent setHealth;
    public HealthUIEvent updateHealth;

    [SerializeField]
    float maxHealth;

    [SerializeField]
    public float health;

    public void Start()
    {
        health = maxHealth;
        if (setHealth != null)
            setHealth.Invoke(maxHealth);
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        UpdateHealth();
    }

    public void Heal(float healing)
    {
        health += healing;
        UpdateHealth();
    }

    public void ResetHealth()
    {
        health = maxHealth;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (updateHealth != null)
            updateHealth.Invoke(health);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DieEvent : UnityEvent<GameObject>
{
}

public class Health : MonoBehaviour
{
    public FillUI healthUI;

    [SerializeField]
    float maxHealth;

    [SerializeField]
    public float health;

    public DieEvent onDied;

    public void Start()
    {
        onDied.AddListener(GameObject.FindGameObjectWithTag("Spellspace").GetComponent<Spellspace>().SomethingDied);
        health = maxHealth;
        if (healthUI != null)
            healthUI.SetFill(maxHealth);
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        UpdateHealth();
        if (health <= 0f)
            onDied?.Invoke(gameObject);
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
        if (healthUI != null)
            healthUI.UpdateFill(health);
    }
}

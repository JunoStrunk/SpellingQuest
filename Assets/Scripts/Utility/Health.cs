using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public FillUI healthUI;

    [SerializeField]
    float maxHealth;

    [SerializeField]
    public float health;

    public void Start()
    {
        health = maxHealth;
        if (healthUI != null)
            healthUI.SetFill(maxHealth);
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
        if (healthUI != null)
            healthUI.UpdateFill(health);
    }
}

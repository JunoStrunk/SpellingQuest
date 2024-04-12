using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public Spell spell;
    ParticleSystem effect;

    void Awake()
    {
        effect = GetComponentInChildren<ParticleSystem>();
        effect.Stop();
    }

    public void cast(Spell spell)
    {
        if (this.spell.incantation == spell.incantation)
            effect.Play();
    }

    public void cast(int tempint, string tempstring)
    {
        effect.Play();
    }
}

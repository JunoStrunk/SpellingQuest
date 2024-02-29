using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    ParticleSystem effect;

    void Awake()
    {
        effect = GetComponent<ParticleSystem>();
        effect.Stop();
    }

    public void cast(Spell spell)
    {
        effect.Play();
    }

    public void cast(int tempint, string tempstring)
    {
        effect.Play();
    }
}

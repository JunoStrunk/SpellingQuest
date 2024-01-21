using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellspace : MonoBehaviour
{
    public void AddSpell(Spell spell)
    {
        Debug.Log(spell.spellName);
        AttackSpell attackSpell = spell as AttackSpell;
        if (attackSpell)
            attackSpell.attack.Damage();
        else
            Debug.Log(spell.spellName + " is not an attack spell");
    }
}

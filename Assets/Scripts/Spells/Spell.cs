using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Battle/Spells/Spell")]
public class Spell : ScriptableObject
{
    public bool isSpecial = false;
    public string spellName;
    public string incantation;

    public enum SpellType
    {
        Attack,
        Avoid,
        Heal
    }
    public SpellType spellType;

}

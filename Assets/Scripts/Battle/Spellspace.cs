using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellspace : MonoBehaviour
{
    public EnemyStats activeEnemy; //Replace with list later on??
    public PlayerStats activePlayer;

    public SceneManage sceneManage;

    public void EnemyAttack(Attack attack)
    {
        attack.Damage();
        activePlayer.Damage(attack.damage);
    }

    public void PlayerSpell(Spell spell)
    {
        Debug.Log(spell.spellName);
        if (spell.spellType == Spell.SpellType.Attack)
        {
            AttackSpell attackSpell = spell as AttackSpell;
            activeEnemy.Damage(attackSpell.attack.damage);
        }
        else if (spell.spellType == Spell.SpellType.Avoid)
        {
            activePlayer.Dodge();
        }
        else if (spell.spellType == Spell.SpellType.Heal)
        {
            HealSpell healSpell = spell as HealSpell;
            activePlayer.Heal(healSpell.healing);
        }
    }

    public void SomethingDied(GameObject thing)
    {
        if (thing.CompareTag("Player"))
            sceneManage.LoadLose();
        else
            sceneManage.LoadWin();

    }
}

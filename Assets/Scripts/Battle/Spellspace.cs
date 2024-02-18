using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spellspace : MonoBehaviour
{
    public EnemyStats activeEnemy; //Replace with list later on??
    public PlayerStats activePlayer;

    public TurnControl turnControl;

    public SceneManage sceneManage;

    void Start()
    {
        sceneManage = GameObject.Find("Managers").GetComponent<SceneManage>();
    }

    public void EnemyAttack(Attack attack)
    {
        attack.Damage();
        activePlayer.Damage(attack.damage);
        TryChangeTurn();
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
        TryChangeTurn();
    }

    public void GenSpell(int dmg)
    {
        Debug.Log(dmg + " General Spell!");
        activeEnemy.Damage(dmg);
        TryChangeTurn();
    }

    public void SomethingDied(GameObject thing)
    {
        if (thing.CompareTag("Player"))
            sceneManage.LoadLose();
        else
            sceneManage.LoadWin();

    }

    void TryChangeTurn()
    {
        turnControl.changeTurn();
    }
}

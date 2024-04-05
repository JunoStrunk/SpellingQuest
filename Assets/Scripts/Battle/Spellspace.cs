using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Spellspace : MonoBehaviour
{
    [SerializeField]
    public bool runningBattle = false;
    List<EnemyStats> TotalEnemies;
    public EnemyStats activeEnemy;
    public PlayerStats activePlayer;

    public TurnControl turnControl;

    bool checkingDied = false;

    void Start()
    {
        checkingDied = false;
        TotalEnemies = new List<EnemyStats>();
    }

    public void EnemyAttack(Attack attack)
    {
        attack.Damage();
        activePlayer.Damage(attack.damage);
        TryChangeTurn();
    }

    public void PlayerSpell(Spell spell)
    {
        if (spell.spellType == Spell.SpellType.Attack)
        {
            AttackSpell attackSpell = spell as AttackSpell;
            SetActiveEnemy(spell.incantation);
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
        //if not airtime
        if (TurnControl.isPlayerTurn)
            TryChangeTurn();
    }

    public void GenSpell(int dmg, string spell)
    {
        SetActiveEnemy(spell);
        activeEnemy.Damage(dmg);
        TryChangeTurn();
    }

    public void SomethingDied(GameObject thing)
    {
        checkingDied = true;
        if (thing.CompareTag("Player"))
            GeneralEventManager.current.PlayerDeath();
        else
        {
            GeneralEventManager.current.LoadDungeon(SceneType.BackToDungeon, 0);
            GeneralEventManager.current.EnemyDefeat();
        }

    }

    void SetActiveEnemy(string spell)
    {
        if (!runningBattle)
            return;

        foreach (EnemyStats enemy in TotalEnemies)
        {
            if (enemy.visible && enemy.word == spell)
            {
                activeEnemy = enemy;
                break;
            }
        }

    }

    void TryChangeTurn()
    {
        if (!checkingDied)
            turnControl.changeTurn();
    }
}

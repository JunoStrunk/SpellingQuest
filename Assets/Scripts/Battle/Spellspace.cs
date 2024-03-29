using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Spellspace : MonoBehaviour
{
    [SerializeField]
    public bool runningBattle = false;
    List<EnemyStats> TotalEnemies;
    public EnemyStats activeEnemy;
    public PlayerStats activePlayer;

    public TurnControl turnControl;

    UnityEvent onReturnDungeon;
    UnityEvent onPlayerDeath;

    [SerializeField]
    GameObject transition;

    bool checkingDied = false;

    void Start()
    {
        checkingDied = false;
        if (onReturnDungeon == null)
            onReturnDungeon = new UnityEvent();
        if (onPlayerDeath == null)
            onPlayerDeath = new UnityEvent();
        TotalEnemies = new List<EnemyStats>();
        onPlayerDeath.AddListener(GameObject.Find("SceneManager").GetComponent<SceneManage>().LoadLose);
        onReturnDungeon.AddListener(transition.GetComponent<Transition>().LoadDungeonNonEnum);
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
        Debug.Log(dmg + " General Spell!");
        activeEnemy.Damage(dmg);
        TryChangeTurn();
    }

    public void SomethingDied(GameObject thing)
    {
        checkingDied = true;
        if (thing.CompareTag("Player"))
            onPlayerDeath.Invoke();
        else
            onReturnDungeon.Invoke();

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

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
    BattleSceneManager scene;

    [SerializeField]
    GameObject transition;

    void Start()
    {
        if (onReturnDungeon == null)
            onReturnDungeon = new UnityEvent();
        TotalEnemies = new List<EnemyStats>();
        scene = GameObject.Find("SceneManager").GetComponent<BattleSceneManager>();
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
        if (thing.CompareTag("Player"))
            scene.LoadLose();
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
        turnControl.changeTurn();
    }
}

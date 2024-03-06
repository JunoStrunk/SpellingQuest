using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AttackEvent : UnityEvent<Attack>
{
}

public class EnemyAttack : MonoBehaviour
{
    EnemyStats enemyStats;

    [SerializeField]
    float attackPause;

    [SerializeField]
    bool continuousAttack;

    [SerializeField]
    List<Attack> attacks;

    AttackEvent onAttack;

    Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        if (onAttack == null)
            onAttack = new AttackEvent();
        onAttack.AddListener(GameObject.FindGameObjectWithTag("Spellspace").GetComponent<Spellspace>().EnemyAttack);
        enemyStats = GetComponent<EnemyStats>();
        if (enemyStats.attackTimer != null)
            enemyStats.attackTimer.SetFill(1.0f);
    }

    public void Attack()
    {
        if (!PauseControl.gameIsPaused && !TurnControl.isPlayerTurn)
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        anim.SetTrigger("Attack");
        Attack currAttack = attacks[UnityEngine.Random.Range(0, attacks.Count)];
        UIEventManager.current.EnemyAttack();
        onAttack?.Invoke(currAttack);
    }

    public IEnumerator AttackTimer()
    {
        // yield return new WaitForSeconds(attackPause);
        float currTime = 0;
        while (currTime <= attackPause)
        {
            currTime += Time.deltaTime;
            if (enemyStats.attackTimer != null)
                enemyStats.attackTimer.UpdateFill(currTime / attackPause);
            yield return null;
        }
        Attack();
        if (enemyStats.attackTimer != null)
            enemyStats.attackTimer.SetFill(1.0f);
        if (continuousAttack)
            StartCoroutine(AttackTimer());
        yield return null;
    }

    public void StartStopTimer()
    {
        if (!TurnControl.isPlayerTurn)
            StartCoroutine(AttackTimer());
        else
        {
            StopCoroutine(AttackTimer());
            if (enemyStats.attackTimer != null)
                enemyStats.attackTimer.SetFill(1.0f);
        }
    }

}

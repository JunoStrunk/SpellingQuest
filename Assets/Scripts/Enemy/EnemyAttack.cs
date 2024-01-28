using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyStats enemyStats;
    bool aggroed;

    [SerializeField]
    float attackPause;

    [SerializeField]
    List<Attack> attacks;

    public void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.attackTimer.SetFill(1.0f);
        StartCoroutine(AttackTimer());
    }

    public void Activate(bool aggroed)
    {
        this.aggroed = aggroed;
    }

    public void Attack()
    {
        if (aggroed && !PauseControl.gameIsPaused)
        {
            Attack currAttack = attacks[Random.Range(0, attacks.Count)];
            EventManager.Instance.InvokeAttackEvent(enemyStats.GetID(), currAttack);
            StartCoroutine(AttackTimer());
        }
    }

    public IEnumerator AttackTimer()
    {
        aggroed = false;
        // yield return new WaitForSeconds(attackPause);
        float currTime = 0;
        while (currTime <= attackPause)
        {
            currTime += Time.deltaTime;
            enemyStats.attackTimer.UpdateFill(currTime / attackPause);
            yield return null;
        }
        aggroed = true;
        enemyStats.attackTimer.SetFill(1.0f);
        yield return null;
    }

    public void Update()
    {
        Attack();
    }
}

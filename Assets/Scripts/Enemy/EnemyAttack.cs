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
    bool aggroed;

    [SerializeField]
    float attackPause;

    [SerializeField]
    List<Attack> attacks;

    AttackEvent onAttack;

    public void Start()
    {
        if (onAttack == null)
            onAttack = new AttackEvent();
        onAttack.AddListener(GameObject.FindGameObjectWithTag("Spellspace").GetComponent<Spellspace>().EnemyAttack);
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
            Attack currAttack = attacks[UnityEngine.Random.Range(0, attacks.Count)];
            onAttack?.Invoke(currAttack);
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

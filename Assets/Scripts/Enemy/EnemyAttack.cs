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
    List<Attack> attacks;

    AttackEvent onAttack;

    public void Awake()
    {
        if (onAttack == null)
            onAttack = new AttackEvent();
        onAttack.AddListener(GameObject.FindGameObjectWithTag("Spellspace").GetComponent<Spellspace>().EnemyAttack);
        enemyStats = GetComponent<EnemyStats>();
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
        Attack currAttack = attacks[UnityEngine.Random.Range(0, attacks.Count)];
        onAttack?.Invoke(currAttack);
    }

    public IEnumerator AttackTimer()
    {
        // yield return new WaitForSeconds(attackPause);
        float currTime = 0;
        while (currTime <= attackPause)
        {
            currTime += Time.deltaTime;
            enemyStats.attackTimer.UpdateFill(currTime / attackPause);
            yield return null;
        }
        Attack();
        enemyStats.attackTimer.SetFill(1.0f);
        yield return null;
    }

    public void StartStopTimer()
    {
        if (!TurnControl.isPlayerTurn)
            StartCoroutine(AttackTimer());
        else
        {
            StopCoroutine(AttackTimer());
            enemyStats.attackTimer.SetFill(1.0f);
        }
    }

}

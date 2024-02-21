using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnControl : MonoBehaviour
{
    [SerializeField]
    bool turnBased;
    static UnityEvent onTurnChange;
    public static bool isPlayerTurn;
    public void changeTurn()
    {
        if (!turnBased)
            return;

        isPlayerTurn = !isPlayerTurn;
        Debug.Log(isPlayerTurn);
        onTurnChange.Invoke();
    }

    void Start()
    {
        if (!turnBased)
            return;

        if (onTurnChange == null)
            onTurnChange = new UnityEvent();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            onTurnChange.AddListener(enemy.GetComponent<EnemyAttack>().StartStopTimer);
        }
        isPlayerTurn = true;
    }
}

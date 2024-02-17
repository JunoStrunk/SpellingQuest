using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnControl : MonoBehaviour
{
    static UnityEvent onTurnChange;
    public static bool isPlayerTurn = true;
    public void changeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        Debug.Log(isPlayerTurn);
        onTurnChange.Invoke();
    }

    void Start()
    {
        if (onTurnChange == null)
            onTurnChange = new UnityEvent();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            onTurnChange.AddListener(enemy.GetComponent<EnemyAttack>().StartStopTimer);
        }
        Debug.Log(isPlayerTurn);
    }
}

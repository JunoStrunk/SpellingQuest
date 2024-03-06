using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    [SerializeField]
    public List<Transform> teleportPoints;

    [SerializeField]
    GameObject player;
    int iter;

    void Start()
    {
        iter = 0;
        UIEventManager.current.onEnemyAttack += TeleportToNext;
    }

    void OnDisable()
    {
        UIEventManager.current.onEnemyAttack -= TeleportToNext;
    }

    void OnDestroy()
    {
        UIEventManager.current.onEnemyAttack -= TeleportToNext;
    }

    public void TeleportToNext()
    {
        if (iter < teleportPoints.Count)
        {
            player.transform.position = teleportPoints[iter].position;
            iter++;
        }
    }
}

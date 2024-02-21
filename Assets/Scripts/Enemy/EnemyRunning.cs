using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRunning : MonoBehaviour
{
    [SerializeField]
    float minDist;
    [SerializeField]
    float speed;
    [SerializeField]
    Transform target;
    EnemyAttack attack;

    void Start()
    {
        attack = GetComponent<EnemyAttack>();
        StartCoroutine(RunTowards());
    }

    public IEnumerator RunTowards()
    {
        while (Vector3.Distance(transform.position, target.position) >= minDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
        attack.StartStopTimer();
        yield return null;
    }

}

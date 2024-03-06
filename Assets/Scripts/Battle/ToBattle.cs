using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ToBattle : MonoBehaviour
{
    UnityEvent onToBattle;

    Animator enemy;
    SpriteRenderer ren;

    void Start()
    {
        if (onToBattle == null)
            onToBattle = new UnityEvent();
        enemy = GetComponent<Animator>();
        ren = GetComponent<SpriteRenderer>();
        ren.enabled = false;
        onToBattle.AddListener(GameObject.Find("Transition").GetComponent<Transition>().LoadBattleNonEnum);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ren.enabled = true;
            other.GetComponent<PlayerMovement>().CanPlayerMove(false);
            enemy.SetTrigger("Appear");
        }
    }

    public void LoadBattle()
    {
        onToBattle.Invoke();
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ToBattle : MonoBehaviour
{
    UnityEvent onToBattle;

    Animator enemy;

    void Start()
    {
        if (onToBattle == null)
            onToBattle = new UnityEvent();
        enemy = GetComponent<Animator>();
        transform.GetChild(0).gameObject.SetActive(false);
        onToBattle.AddListener(GameObject.Find("Transition").GetComponent<Transition>().LoadBattleNonEnum);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().CanPlayerMove(false);
            transform.GetChild(0).gameObject.SetActive(true);
            enemy.SetTrigger("Appear");
        }
    }

    public void LoadBattle()
    {
        onToBattle.Invoke();
        Destroy(this.gameObject);
    }
}

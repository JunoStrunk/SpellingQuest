using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ToBattle : MonoBehaviour
{
    [SerializeField]
    int battleID = 4;
    Animator enemy;

    void Start()
    {
        enemy = GetComponent<Animator>();
        transform.GetChild(0).gameObject.SetActive(false);
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
        GeneralEventManager.current.LoadBattle(SceneType.ToBattle, battleID);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToBattle : MonoBehaviour
{
    private BattleSceneManager scene;

    Animator enemy;
    SpriteRenderer ren;

    void Start()
    {
        enemy = GetComponent<Animator>();
        ren = GetComponent<SpriteRenderer>();
        ren.enabled = false;
        scene = GameObject.Find("SceneManager").GetComponent<BattleSceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ren.enabled = true;
            enemy.SetTrigger("Appear");
        }
    }

    public void LoadBattle()
    {
        scene.LoadBattle();
        Destroy(this.gameObject);
    }
}

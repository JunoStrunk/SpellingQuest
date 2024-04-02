using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    // Beat all enemies
    // Hit a collider?
    // Have collected a number of items?

    enum WinType
    {
        BeatEnemies,
        HitCollider,
        CollectedItems
    };

    [SerializeField]
    WinType winType = new WinType();

    delegate void FunctionalWin();
    FunctionalWin functionWin;

    int macguffins;

    [SerializeField]
    int collectableID;

    void Start()
    {
        if (winType == WinType.BeatEnemies)
        {
            GeneralEventManager.current.onEnemyDefeat += DecreaseMacguffins;
            macguffins = GameObject.FindGameObjectsWithTag("EnemyDungeon").Length;
            Debug.Log("Curr Macguffins" + macguffins);
        }
        else if (winType == WinType.CollectedItems)
        {
            GeneralEventManager.current.onItemCollect += DecreaseMacguffinsID;
            GameObject[] possMacguffins = GameObject.FindGameObjectsWithTag("Item");
            // foreach (GameObject possMacguffin in possMacguffins)
            //     if (possMacguffin.GetComponent<Item>().getID() == collectableID)
            //         macguffins++;

        }

    }

    void OnDestroy()
    {
        if (winType == WinType.BeatEnemies)
        {
            GeneralEventManager.current.onEnemyDefeat -= DecreaseMacguffins;
        }
        else if (winType == WinType.CollectedItems)
        {
            GeneralEventManager.current.onItemCollect -= DecreaseMacguffinsID;

        }
    }

    void DecreaseMacguffins()
    {
        macguffins--;
        Debug.Log(macguffins);
        if (macguffins <= 0)
            GeneralEventManager.current.Win();
    }

    void DecreaseMacguffinsID(int id)
    {
        Debug.Log("I'm also being called for some reason");
        if (id != collectableID)
            return;
        macguffins--;
        if (macguffins <= 0)
            GeneralEventManager.current.Win();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GeneralEventManager.current.Win();
    }


}

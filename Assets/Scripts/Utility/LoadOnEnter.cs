using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnEnter : MonoBehaviour
{
    [SerializeField]
    SceneType sceneType = new SceneType();

    [SerializeField]
    int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneType == SceneType.ToBattle)
                GeneralEventManager.current.LoadBattle(sceneType, id);
            else if (sceneType == SceneType.ToDungeon)
                GeneralEventManager.current.LoadDungeon(sceneType, id);
        }
    }
}

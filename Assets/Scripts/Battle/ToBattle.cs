using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToBattle : MonoBehaviour
{
    private SceneManage scene;

    void Start()
    {
        scene = GameObject.Find("Managers").GetComponent<SceneManage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scene.LoadBattle();
        }
    }
}

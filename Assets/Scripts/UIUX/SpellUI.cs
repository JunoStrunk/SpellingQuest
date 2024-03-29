using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellUI : MonoBehaviour
{

    [SerializeField]
    List<GameObject> TabUIs;

    [SerializeField]
    List<Animator> Tabs;

    int savedTab = 0;

    void Start()
    {
        for (int i = 1; i < TabUIs.Count; i++)
        {
            TabUIs[i].SetActive(false);
            Tabs[i].SetBool("Open", false);
        }
    }

    void OnEnable()
    {
        if (TabUIs.Count > 0)
        {
            TabUIs[savedTab].SetActive(true);
            Tabs[savedTab].SetBool("Open", true);
        }
    }

    public void toggleTab(int index)
    {
        if (TabUIs.Count <= 0)
            return;
        TabUIs[index].SetActive(true);
        Tabs[index].SetBool("Open", true);
        savedTab = index;
        for (int i = 0; i < TabUIs.Count; i++)
        {
            if (i == index)
                continue;
            TabUIs[i].SetActive(false);
            Tabs[i].SetBool("Open", false);
        }
    }
}

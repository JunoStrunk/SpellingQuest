using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TabEvent : UnityEvent<int>
{

}

public class SpellTabs : MonoBehaviour
{
    [SerializeField]
    int id;
    TabEvent onTabPress;
    void Start()
    {
        if (onTabPress == null)
            onTabPress = new TabEvent();
        onTabPress.AddListener(transform.parent.GetComponent<SpellUI>().toggleTab);
    }

    public void toggle()
    {
        onTabPress.Invoke(id);
    }
}

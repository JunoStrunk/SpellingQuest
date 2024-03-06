using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UsedSpellsUI : MonoBehaviour
{
    public GameObject SpellText;

    void Start()
    {
        UIEventManager.current.onUsedSpell += AddUsedSpell;
    }

    void OnDestroy()
    {
        UIEventManager.current.onUsedSpell -= AddUsedSpell;
    }

    void AddUsedSpell(string spell)
    {
        GameObject newSpellText = Instantiate(SpellText, this.transform);
        newSpellText.GetComponent<TMP_Text>().text = spell;
    }
}

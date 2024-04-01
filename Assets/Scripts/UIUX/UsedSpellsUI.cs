using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UsedSpellsUI : MonoBehaviour
{
    public GameObject SpellText;
    public Transform usedSpellContainer;

    public void Start()
    {
        UIEventManager.current.onUsedSpell += AddUsedSpell;
    }

    void OnDestroy()
    {
        UIEventManager.current.onUsedSpell -= AddUsedSpell;
    }

    void AddUsedSpell(string spell)
    {
        Debug.Log(spell + "Called to Used Spells");
        GameObject newSpellText = Instantiate(SpellText, usedSpellContainer);
        newSpellText.GetComponent<TMP_Text>().text = spell;
    }
}

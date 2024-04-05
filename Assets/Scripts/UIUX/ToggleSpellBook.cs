using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSpellBook : MonoBehaviour
{
    [SerializeField]
    GameObject spellBookUI;

    public void ShowSpellBook()
    {
        spellBookUI.SetActive(true);
    }
    public void HideSpellBook()
    {
        spellBookUI.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TMP_Text keyText;
    private char key;

    [Header(" Settings ")]
    [SerializeField] private bool isBackspace;
    [SerializeField] private bool isSpace;
    [SerializeField] private bool isEnter;

    public void SetKey(char key)
    {
        this.key = key;
        keyText.text = key.ToString();
    }

    public Button GetButton()
    {
        return GetComponent<Button>();
    }

    public bool IsBackspace()
    {
        return isBackspace;
    }
    public bool IsSpace()
    {
        return isSpace;
    }
    public bool IsEnter()
    {
        return isEnter;
    }
}

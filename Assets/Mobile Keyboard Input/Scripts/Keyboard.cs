using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    [Header(" Elements ")]
    private VerticalLayoutGroup vertPanel;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject horzPanel;
    [SerializeField] private Key keyPrefab;
    [SerializeField] private Key backspaceKeyPrefab;
    [SerializeField] private Key spaceKeyPrefab;
    [SerializeField] private Key enterKeyPrefab;


    [Header(" Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float widthPercent;
    [Range(0f, 1f)]
    [SerializeField] private float heightPercent;
    [Range(0f, .5f)]
    [SerializeField] private float bottomOffset;


    [Header(" Keyboard Lines ")]
    [SerializeField] private KeyboardLine[] lines;


    [Header(" Key Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float keyToLineRatio;
    [Range(0f, 1f)]
    [SerializeField] private float keyXSpacing;


    [Header(" Events ")]
    public Action<char> onKeyPressed;
    public Action onBackspacePressed;
    public Action onSpacePressed;
    public Action onEnterPressed;

    // Start is called before the first frame update
    void Start()
    {
        vertPanel = transform.GetChild(0).GetComponent<VerticalLayoutGroup>();
        CreateKeys();

        //rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
    }

    private void CreateKeys()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            GameObject newHorzPanel = Instantiate(horzPanel, vertPanel.transform);
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                char key = lines[i].keys[j];

                if (key == '.')
                {
                    // It's the backspace key
                    Key keyInstance = Instantiate(backspaceKeyPrefab, newHorzPanel.transform);

                    keyInstance.GetButton().onClick.AddListener(() => BackspacePressedCallback());
                }
                else if (key == '_')
                {
                    // It's the space key
                    Key keyInstance = Instantiate(spaceKeyPrefab, newHorzPanel.transform);

                    keyInstance.GetButton().onClick.AddListener(() => SpacePressedCallback());

                }
                else if (key == '<')
                {
                    // It's the enter key
                    Key keyInstance = Instantiate(enterKeyPrefab, newHorzPanel.transform);

                    keyInstance.GetButton().onClick.AddListener(() => EnterPressedCallback());

                }
                else
                {
                    // It's a normal key
                    Key keyInstance = Instantiate(keyPrefab, newHorzPanel.transform);
                    keyInstance.SetKey(key);

                    keyInstance.GetButton().onClick.AddListener(() => KeyPressedCallback(key));
                }


            }
        }
    }

    private void BackspacePressedCallback()
    {
        Debug.Log("Backspace pressed");

        onBackspacePressed?.Invoke();
    }
    private void SpacePressedCallback()
    {
        Debug.Log("Space pressed");

        onSpacePressed?.Invoke();
    }
    private void EnterPressedCallback()
    {
        Debug.Log("Enter pressed");

        onEnterPressed?.Invoke();
    }

    private void KeyPressedCallback(char key)
    {
        Debug.Log("Key pressed : " + key);

        onKeyPressed?.Invoke(key);
    }
}

[System.Serializable]
public struct KeyboardLine
{
    public string keys;
}
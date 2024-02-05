using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TouchEvent : UnityEvent<Letter>
{
}

public class Letter : EventTrigger
{
    [HideInInspector]
    public TouchEvent onLetterSelect;
    [HideInInspector]
    public TouchEvent onLetterDeselect;

    bool isHighlighted = false;
    bool hovering = false;
    RectTransform rect;
    Image img;

    [Header("Data")]
    [SerializeField]
    public char character;

    [Header("Colors")]
    public Color orgColor;
    public Color highlightColor;


    void Start()
    {
        character = transform.GetChild(0).GetComponent<TMP_Text>().text[0];
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        orgColor = img.color;
        onLetterSelect.AddListener(transform.parent.GetComponent<LineBehavior>().AddPoint);
        onLetterSelect.AddListener(GameObject.Find("Spellbook").GetComponent<Spellbook>().AddLetter);
        onLetterDeselect.AddListener(transform.parent.GetComponent<LineBehavior>().RemovePoint);
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        if (!isHighlighted && !hovering)
        {
            img.color = highlightColor;
            isHighlighted = true;
            hovering = true;
            onLetterSelect?.Invoke(this);
        }
    }

    public override void OnPointerExit(PointerEventData data)
    {
        hovering = false;
    }

    public void Deselect()
    {
        if (isHighlighted)
        {
            img.color = orgColor;
        }
        isHighlighted = false;
        hovering = false;
        onLetterDeselect?.Invoke(this);
    }

    public Vector2 GetPosition()
    {
        return rect.position;
    }
}

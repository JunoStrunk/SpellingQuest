using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterMaker : MonoBehaviour
{
    public GameObject letterPrefab;

    [SerializeField]
    RWMagicCircle rw;

    // [SerializeField]
    List<char> letters = new List<char>();

    public float radius;
    public float scaleMultiplier;

    //Private
    float angle = 0f;

    void Awake()
    {
        MakeLetters();
    }

    public void MakeLetters()
    {
        letters = rw.RMagicCircle();
        if (letters.Count <= 0)
            return;
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        RectTransform rect = GetComponent<RectTransform>();
        float angleInc = 360 / letters.Count;
        for (int i = 0; i < letters.Count; i++)
        {
            Vector3 letterPos = new Vector3(rect.anchoredPosition.x + radius * Mathf.Cos(angle), rect.anchoredPosition.y + radius * Mathf.Sin(angle), 0f);
            GameObject newLetter = Instantiate(letterPrefab, rect, true);
            newLetter.transform.localPosition = new Vector3(radius * Mathf.Cos((angle + 90) * Mathf.Deg2Rad), radius * Mathf.Sin((angle + 90) * Mathf.Deg2Rad), 0f);
            newLetter.transform.localScale = new Vector3(1f * scaleMultiplier, 1f * scaleMultiplier, 1f);
            newLetter.transform.GetChild(0).GetComponent<TMP_Text>().text = letters[i].ToString();
            angle += angleInc;
        }
    }
}

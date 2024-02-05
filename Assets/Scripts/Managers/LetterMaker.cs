using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterMaker : MonoBehaviour
{
    public GameObject letterPrefab;

    [SerializeField]
    List<char> letters;

    public float radius;
    public float scaleMultiplier;

    //Private
    float angle = 0f;

    void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float angleInc = 360 / letters.Count;
        Debug.Log(angleInc);
        for (int i = 0; i < letters.Count; i++)
        {
            Vector3 letterPos = new Vector3(rect.anchoredPosition.x + radius * Mathf.Cos(angle), rect.anchoredPosition.y + radius * Mathf.Sin(angle), 0f);
            GameObject newLetter = Instantiate(letterPrefab, rect, true);
            Debug.Log($"Radius: {radius} | Angle: {angle} | Cos: {Mathf.Cos(angle * Mathf.Deg2Rad)} | Sin: {Mathf.Sin(angle * Mathf.Deg2Rad)}");
            newLetter.transform.localPosition = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
            newLetter.transform.localScale = new Vector3(1f * scaleMultiplier, 1f * scaleMultiplier, 1f);
            newLetter.transform.GetChild(0).GetComponent<TMP_Text>().text = letters[i].ToString();
            angle += angleInc;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StonesParent : MonoBehaviour
{
    [SerializeField]
    List<char> letters;

    [SerializeField]
    RWMagicCircle rw;

    float MinX, MinY, MaxX, MaxY;
    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        rect.GetLocalCorners(corners);

        MinX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        MinY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        MaxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        MaxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

        string mcLetters = charListToString(rw.RMagicCircle());
        List<RectTransform> children = new List<RectTransform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }

        RectTransform circleParent = transform.parent.GetChild(1).GetComponent<RectTransform>();
        int circleChildIdx = 0;

        foreach (RectTransform child in children)
        {
            char letter = child.GetComponentInChildren<TMP_Text>().text[0];
            if (mcLetters.Contains(letter)) //if stone equals a letter
            {
                //set it to the next available snapping position
                RectTransform sp = circleParent.GetChild(circleChildIdx).GetComponent<RectTransform>();
                Vector2 newPos = new Vector2(sp.anchoredPosition.x, sp.anchoredPosition.y - rect.anchoredPosition.y + circleParent.anchoredPosition.y);
                child.anchoredPosition = newPos;
                circleChildIdx++;

                //and remove from possible letters (for duplicate letters)
                mcLetters = RemoveFromString(letter, mcLetters);
            }
            else //otherwise set to a random position within the defined range
            {
                child.anchoredPosition = new Vector3(UnityEngine.Random.Range(MinX, MaxX), UnityEngine.Random.Range(MinY, MaxY), 0.0f);
            }
        }

        //TODO: Time this out later to put on resume
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     bool atSnapPoint = false;
        //     for (int j = 0; j < letters.Length; j++)
        //     {
        //         // if the stone equals a letter
        //         if (transform.GetChild(i).GetComponentInChildren<TMP_Text>().text[0].Equals(letters[j]))
        //         {
        //             //Set it to the next available snapping position
        //             RectTransform sp = circleParent.GetChild(circleChildIdx).GetComponent<RectTransform>();
        //             Vector2 newPos = new Vector2(sp.anchoredPosition.x, sp.anchoredPosition.y - rect.anchoredPosition.y + circleParent.anchoredPosition.y);
        //             transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = newPos;
        //             circleChildIdx++;
        //             atSnapPoint = true;

        //             //remove letter from letters, for duplicate letters
        //             letters = letters.Remove(j, 1);
        //         }
        //     }

        //     // if stone hasn't been snapped, place randomly in space
        //     if (!atSnapPoint)
        //         transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(UnityEngine.Random.Range(MinX, MaxX), UnityEngine.Random.Range(MinY, MaxY), 0.0f);
        // }

        StartCoroutine(CheckSnaps(circleParent.GetComponent<Snapping>()));
    }

    string charListToString(List<char> list)
    {
        string returnString = "";
        foreach (Char c in list)
            returnString += c;
        return returnString;
    }

    //Thank you to LukeH on stackoverflow for this code
    string RemoveFromString(char c, string sourceString)
    {
        int index = sourceString.IndexOf(c);
        string cleanPath = (index < 0)
            ? sourceString
            : sourceString.Remove(index, 1);
        return sourceString;
    }

    IEnumerator CheckSnaps(Snapping circlePnt)
    {
        yield return new WaitForSeconds(2f);

        Transform child;
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            circlePnt.CheckSnap(child.position, child);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ColorTurnTest : MonoBehaviour
{
    SpriteRenderer spriteren;

    public void Start()
    {
        spriteren = GetComponent<SpriteRenderer>();
    }

    public void TurnColor(float timing)
    {
        StartCoroutine(ColorTiming(timing));
    }

    private IEnumerator ColorTiming(float timing)
    {
        spriteren.color = Color.red;
        yield return new WaitForSeconds(timing);
        spriteren.color = Color.white;
    }
}

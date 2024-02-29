using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempShowing : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.enabled = false;
    }

    public IEnumerator ShowTemp()
    {
        text.enabled = true;
        yield return new WaitForSeconds(.9f);
        text.enabled = false;
    }
}

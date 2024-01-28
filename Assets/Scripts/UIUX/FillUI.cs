using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FillUI : MonoBehaviour
{
    UnityEngine.UI.Image fillBar;

    float maxFill;

    public void Start()
    {
        fillBar = GetComponent<UnityEngine.UI.Image>();
    }

    public void SetFill(float currFill)
    {
        maxFill = currFill;
    }

    public void UpdateFill(float currFill)
    {
        float fillAmount = currFill / maxFill;
        if (fillAmount > 1)
        {
            fillAmount = 1.0f;
        }

        fillBar.fillAmount = fillAmount;
    }


}

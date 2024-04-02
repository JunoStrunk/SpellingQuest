using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonesParent : MonoBehaviour
{
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

        Debug.Log(MinX + " " + MinY + " " + MaxX + " " + MaxY);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), 0.0f);
        }
    }


}

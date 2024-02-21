using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIFollowGameObject : MonoBehaviour
{
    public RectTransform UIElement;
    public Vector3 Offset;

    bool visible;

    Camera main;

    void Start()
    {
        visible = false;
        main = Camera.current;
    }

    void Update()
    {
        if (visible)
            UIElement.anchoredPosition = main.WorldToScreenPoint(transform.position + Offset);
    }

    void OnBecameVisible()
    {
        visible = true;
    }

    void OnBecameInvisible()
    {
        visible = false;
    }
}

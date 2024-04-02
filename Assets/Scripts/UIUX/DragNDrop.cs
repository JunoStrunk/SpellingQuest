using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IDragHandler, IDropHandler
{
    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData data)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, data.position, data.pressEventCamera, out Vector3 mousePos))
        {
            rect.position = mousePos;
        }
    }

    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
        {
            UIEventManager.current.RockDrop(rect.position, this.transform);
        }
    }
}

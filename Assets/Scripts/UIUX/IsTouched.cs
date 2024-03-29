using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IsTouched : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent circleTouched;

    void Start()
    {
        circleTouched.AddListener(GameObject.Find("Spellbook").GetComponent<Spellbook>().touched);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        circleTouched.Invoke();
    }
}

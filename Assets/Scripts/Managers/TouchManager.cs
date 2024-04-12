using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[System.Serializable]
public class MoveEvent : UnityEvent<Vector3>
{
}

public class TouchManager : MonoBehaviour
{
    DialogueManager dialogue;
    [HideInInspector]
    public MoveEvent onTouchMove;
    [HideInInspector]
    public UnityEvent onTouchEnd;


    private void Awake()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        dialogue = FindObjectOfType<DialogueManager>();
        Transform letterContainer = GameObject.FindGameObjectWithTag("Letters").transform;
        onTouchEnd.AddListener(GameObject.Find("Spellbook").GetComponent<Spellbook>().CollectInput);
        for (int i = 0; i < letterContainer.childCount; i++)
        {
            Letter child = letterContainer.GetChild(i).GetComponent<Letter>();
            onTouchEnd.AddListener(child.Deselect);
        }
    }

    void Update()
    {
        if (dialogue && dialogue.isInDialouge)
            return;
        if (Touch.activeFingers.Count == 1)
        {
            Touch activeTouch = Touch.activeFingers[0].currentTouch;

            if (activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Ended || activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
            {
                onTouchEnd?.Invoke();
            }
        }
    }
}

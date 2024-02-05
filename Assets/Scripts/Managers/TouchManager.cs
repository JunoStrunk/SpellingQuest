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
        Transform letterContainer = GameObject.FindGameObjectWithTag("Letters").transform;
        onTouchEnd.AddListener(GameObject.Find("Spellbook").GetComponent<Spellbook>().CollectInput);
        // onTouchMove.AddListener(letterContainer.GetComponent<LineBehavior>().MovePoint);
        for (int i = 0; i < letterContainer.childCount; i++)
        {
            Letter child = letterContainer.GetChild(i).GetComponent<Letter>();
            onTouchEnd.AddListener(child.Deselect);
        }
    }

    void Update()
    {
        if (Touch.activeFingers.Count == 1)
        {
            Touch activeTouch = Touch.activeFingers[0].currentTouch;

            if (activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                // onTouchMove?.Invoke(activeTouch.screenPosition);
            }
            else if (activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Ended || activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
            {
                onTouchEnd?.Invoke();
            }

            // Debug.Log($"Phase: {activeTouch.phase} | Position: {activeTouch.screenPosition}");
        }
    }
}

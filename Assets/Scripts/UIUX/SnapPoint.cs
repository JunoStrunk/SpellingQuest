using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    Transform currStone;

    public void setCurrStone(Transform c)
    {
        currStone = c;
    }

    public Transform getCurrStone()
    {
        return currStone;
    }
}

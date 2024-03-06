using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountObjects : MonoBehaviour
{
    public int needed;
    private int has;

    void Start()
    {
        has = 0;
    }

    public void complete()
    {
        has++;
    }
}

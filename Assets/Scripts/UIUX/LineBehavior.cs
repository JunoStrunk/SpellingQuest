using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehavior : MonoBehaviour
{
    public LineRenderer line;
    IDictionary<int, Vector3> lineIndices;

    // Start is called before the first frame update
    void Start()
    {
        lineIndices = new Dictionary<int, Vector3>();
    }

    public void AddPoint(Letter letter)
    {
        lineIndices.Add(lineIndices.Count, letter.GetPosition());

        line.positionCount = lineIndices.Count;
        line.SetPosition(lineIndices.Count - 1, letter.GetPosition());
        // isLetterSelected = true;
    }
    public void RemovePoint(Letter letter)
    {
        lineIndices.Remove(lineIndices.Count - 1);
        line.positionCount = lineIndices.Count;
        // isLetterSelected = false;
    }
}

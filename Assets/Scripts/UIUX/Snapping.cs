using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    [SerializeField]
    float tolerance = 1f;

    List<SnapPoint> points = new List<SnapPoint>();

    void Start()
    {
        UIEventManager.current.onRockDrop += CheckSnap;
        for (int i = 0; i < transform.childCount; i++)
        {
            points.Add(transform.GetChild(i).GetComponent<SnapPoint>());
        }
    }

    void OnDestroy()
    {
        UIEventManager.current.onRockDrop -= CheckSnap;
    }

    void CheckSnap(Vector3 pos, Transform rock)
    {
        foreach (SnapPoint sp in points)
        {
            if (sp.getCurrStone() == rock && Vector3.Distance(sp.GetComponent<RectTransform>().position, pos) > tolerance)
            {
                sp.setCurrStone(null);
            }
            else if (Vector3.Distance(sp.GetComponent<RectTransform>().position, pos) <= tolerance)
            {
                rock.position = sp.transform.position;
                sp.setCurrStone(rock.transform);
            }
        }
    }

    public string GetStoneData()
    {
        string returnString = "";
        foreach (SnapPoint sp in points)
        {
            if (sp.getCurrStone() != null)
                returnString += sp.getCurrStone().GetChild(0).GetComponent<TMP_Text>().text[0];
        }
        return returnString;
    }
}

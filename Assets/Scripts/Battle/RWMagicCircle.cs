using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class RWMagicCircle : MonoBehaviour
{
    [SerializeField]
    Snapping workingCircle;

    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/MagicCircle.txt";
        if (!File.Exists(path))
        {
            Debug.Log("Creating file");
            //If doesn't exist. Create!
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                // Write some text to the file
                sw.Write("FLIERH");
                sw.Close();
            }
        }
    }

    public void SaveMagicCircle()
    {
        WMagicCircle(workingCircle.GetStoneData());
    }

    public List<char> RMagicCircle()
    {
        List<char> chars = new List<char>();
        if (!File.Exists(path))
        {
            Debug.Log(path);
            Debug.LogError("Unable to open magic circle - Read");
            return null;
        }

        StreamReader sr = new StreamReader(path);
        while (sr.Peek() >= 0)
        {
            chars.Add((char)sr.Read());
        }
        sr.Close();
        return chars;
    }

    public bool WMagicCircle(string data)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("Unable to open magic circle - Write");
            return false;
        }

        File.WriteAllText(path, data);
        return true;
    }
}

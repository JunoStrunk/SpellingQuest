using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LetterMaker))]
public class LetterMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LetterMaker letterMaker = (LetterMaker)target;
        if (GUILayout.Button("Spawn Letters"))
        {
            letterMaker.MakeLetters();
        }
    }
}

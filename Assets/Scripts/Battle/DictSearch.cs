using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictSearch : MonoBehaviour
{
	Dictionary<string, int> dict;
	[SerializeField]
	TextAsset dictText;

	void Awake()
	{
		// TextAsset dictText = Resources.Load("dictionary") as TextAsset;
		dict = new Dictionary<string, int>();

		string[] lines = dictText.text.Split('\n');

		for (int i = 0; i < lines.Length; i++)
		{
			// Debug.Log(lines[i]);

			//Mac
			dict.Add(lines[i], lines[i].Length);

			//Windows
			// dict.Add(lines[i].Remove(lines[i].Length - 1, 1), lines[i].Length);
		}
	}

	public int Search(string word)
	{
		int damage = 0;
		if (dict.ContainsKey(word))
			damage = dict[word];
		return damage;
	}

}

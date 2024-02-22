using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
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
			dict.Add(lines[i].Remove(lines[i].Length - 1, 1), lines[i].Length);
		}

		// path = Application.persistentDataPath + "/dictionary.txt";
		// if (File.Exists(path))
		// {
		// 	using (StreamReader sr = File.OpenText(path))
		// 	{
		// 		string s;
		// 		while ((s = sr.ReadLine()) != null)
		// 		{
		// 			dict.Add(s, s.Length);
		// 		}
		// 	}
		// }
		// else
		// {
		// 	Debug.Log("File does not exist");
		// }
	}

	public int Search(string word)
	{
		int damage = 0;
		if (dict.ContainsKey(word))
			damage = dict[word];
		return damage;
	}

}

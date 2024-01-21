using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class SpellEvent : UnityEvent<Spell>
{
}

public class Spellbook : MonoBehaviour
{
	[HideInInspector]
	public SpellEvent castSpell;

	[SerializeField]
	TMP_Text castingArea;

	[SerializeField]
	List<Spell> spells;

	void Start()
	{
		if (castSpell == null)
			castSpell = new SpellEvent();

		castSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().AddSpell);
	}

	public void Update()
	{
		foreach (char c in Input.inputString)
		{
			if (c == '\n' || c == '\r') //If player presses new line or return...
			{
				//Check input if it matches any spells
				ValidateSpell(castingArea.text);

				//Delete text area
				castingArea.text = string.Empty;
			}


			else if (c == '\u0008') //backspace
			{
				if (castingArea.text.Length > 0)
					castingArea.text = castingArea.text.Substring(0, castingArea.text.Length - 1);
			}
			else
			{
				castingArea.text += c;
			}
		}
	}

	public void ValidateSpell(string typedSpell)
	{
		foreach (Spell spell in spells)
		{
			if (typedSpell == spell.incantation)
			{
				castSpell.Invoke(spell);
				break;
			}
		}
	}

}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

[System.Serializable]
public class SpellEvent : UnityEvent<Spell>
{
}

[System.Serializable]
public class GenSpellEvent : UnityEvent<int, string>
{
}

public class Spellbook : MonoBehaviour
{
	[HideInInspector]
	public SpellEvent castSpell;

	[HideInInspector]
	public GenSpellEvent genSpell;

	[SerializeField]
	TMP_InputField castingArea;

	[SerializeField]
	TempShowing usedWordWarning;

	[SerializeField]
	TempShowing noWordWarning;

	[SerializeField]
	List<Spell> spells;
	List<string> usedSpells;
	DictSearch dict;

	void Start()
	{
		dict = GetComponent<DictSearch>();
		if (castSpell == null)
			castSpell = new SpellEvent();
		if (genSpell == null)
			genSpell = new GenSpellEvent();

		usedSpells = new List<string>();

		castingArea.Select();

		castSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().PlayerSpell);
		castSpell.AddListener(GameObject.Find("SpellEffect").GetComponent<SpellEffect>().cast);
		genSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().GenSpell);
		genSpell.AddListener(GameObject.Find("SpellEffect").GetComponent<SpellEffect>().cast);
	}

	public void Update()
	{
		if (!PauseControl.gameIsPaused && Input.GetKeyDown(KeyCode.Return))
			CollectInput();
	}

	public void CollectInput()
	{
		ValidateSpell(castingArea.text.ToLower());
		castingArea.text = string.Empty;
		castingArea.ActivateInputField();
		castingArea.Select();
	}

	public void AddLetter(Letter letter)
	{
		castingArea.text += letter.character;
	}

	public void ValidateSpell(string typedSpell)
	{
		if (PauseControl.gameIsPaused)
			return;


		foreach (Spell spell in spells)
		{
			if (typedSpell == spell.incantation)
			{
				castSpell.Invoke(spell);
				return;
			}
		}

		if (!TurnControl.isPlayerTurn)
			return;

		typedSpell = typedSpell.ToUpper();

		int damage = dict.Search(typedSpell);
		if (damage > 0 && !usedSpells.Contains(typedSpell))
		{
			UIEventManager.current.UsedSpell(typedSpell);
			usedSpells.Add(typedSpell);
			genSpell.Invoke(damage, typedSpell);
		}
		else if (damage > 0)
		{
			StartCoroutine(usedWordWarning.ShowTemp());
		}
		else
		{
			StartCoroutine(noWordWarning.ShowTemp());
		}
	}

}

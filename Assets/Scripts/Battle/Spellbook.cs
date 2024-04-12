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
	SpellEffect genSpellEffect;

	[SerializeField]
	List<SpellEffect> effects;

	[SerializeField]
	List<Spell> spells;
	List<string> usedSpells;
	DictSearch dict;

	bool inCircle = false;

	void Start()
	{
		dict = GetComponent<DictSearch>();
		if (castSpell == null)
			castSpell = new SpellEvent();
		if (genSpell == null)
			genSpell = new GenSpellEvent();

		usedSpells = new List<string>();

		castingArea.Select();

		foreach (SpellEffect effect in effects)
		{
			castSpell.AddListener(effect.cast);
		}
		genSpell.AddListener(genSpellEffect.cast);

		castSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().PlayerSpell);
		genSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().GenSpell);
	}

	public void Update()
	{
		if (!PauseControl.gameIsPaused && Input.GetKeyDown(KeyCode.Return))
		{
			inCircle = true;
			CollectInput();
		}
	}

	public void touched()
	{
		inCircle = true;
	}

	public void CollectInput()
	{
		if (!inCircle)
			return;
		ValidateSpell(castingArea.text.ToLower());
		castingArea.text = string.Empty;
		castingArea.ActivateInputField();
		castingArea.Select();
		inCircle = false;
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

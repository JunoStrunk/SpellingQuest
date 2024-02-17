using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class SpellEvent : UnityEvent<Spell>
{
}

[System.Serializable]
public class GenSpellEvent : UnityEvent<int>
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
	List<Spell> spells;
	DictSearch dict;

	void Start()
	{
		dict = GetComponent<DictSearch>();
		if (castSpell == null)
			castSpell = new SpellEvent();
		if (genSpell == null)
			genSpell = new GenSpellEvent();

		castingArea.Select();

		castSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().PlayerSpell);
		genSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().GenSpell);
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

		if (!TurnControl.isPlayerTurn)
			return;

		foreach (Spell spell in spells)
		{
			if (typedSpell == spell.incantation)
			{
				castSpell.Invoke(spell);
				return;
			}
		}
		int damage = dict.Search(typedSpell.ToUpper());
		if (damage > 0)
			genSpell.Invoke(damage);
	}

}

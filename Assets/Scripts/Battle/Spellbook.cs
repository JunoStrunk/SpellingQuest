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
	TMP_InputField castingArea;

	[SerializeField]
	List<Spell> spells;

	void Start()
	{
		if (castSpell == null)
			castSpell = new SpellEvent();

		castingArea.Select();

		castSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().PlayerSpell);
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

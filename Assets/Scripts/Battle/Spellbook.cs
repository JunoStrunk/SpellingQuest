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
	private Keyboard keyboard;

	[SerializeField]
	List<Spell> spells;

	void Start()
	{
		if (castSpell == null)
			castSpell = new SpellEvent();

		castingArea.Select();

		keyboard.onBackspacePressed += BackspacePressedCallback;
		keyboard.onSpacePressed += SpacePressedCallback;
		keyboard.onEnterPressed += EnterPressedCallback;
		keyboard.onKeyPressed += KeyPressedCallback;

		castSpell.AddListener(GameObject.Find("Spellspace").GetComponent<Spellspace>().PlayerSpell);
	}

	void OnDestroy()
	{
		keyboard.onBackspacePressed -= BackspacePressedCallback;
		keyboard.onSpacePressed -= SpacePressedCallback;
		keyboard.onEnterPressed -= EnterPressedCallback;
		keyboard.onKeyPressed -= KeyPressedCallback;
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
		// foreach (char c in Input.inputString)
		// {
		// 	if (c == '\n' || c == '\r') //If player presses new line or return...
		// 	{
		// 		//Check input if it matches any spells
		// 		ValidateSpell(castingArea.text);

		// 		//Delete text area
		// 		castingArea.text = string.Empty;
		// 	}
		// 	else if (c == '\u0008') //backspace
		// 	{
		// 		if (castingArea.text.Length > 0)
		// 			castingArea.text = castingArea.text.Substring(0, castingArea.text.Length - 1);
		// 	}
		// 	else
		// 	{
		// 		castingArea.text += c;
		// 	}
		// }

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

	private void BackspacePressedCallback()
	{
		if (castingArea.text.Length > 0)
			castingArea.text = castingArea.text.Substring(0, castingArea.text.Length - 1);
	}
	private void SpacePressedCallback()
	{

		castingArea.text += " ";
	}
	private void EnterPressedCallback()
	{
		if (castingArea.text.Length > 0)
		{
			CollectInput();
		}
	}

	private void KeyPressedCallback(char key)
	{
		castingArea.text += key.ToString();
	}

}

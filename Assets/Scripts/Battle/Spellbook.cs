using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    foreach (char c in Input.inputString)
	{
		if (c == '\n' || c == '\r') //If player presses new line or return...
		{
			//Check input if it matches any spells
			spellManager.validateSpell(castingArea.text);

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

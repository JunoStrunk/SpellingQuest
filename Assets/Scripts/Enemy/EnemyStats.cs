using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : IStats
{
	[SerializeField]
	public FillUI attackTimer;

	public string word;
	public bool visible = false;

	void OnBecameVisible()
	{
		visible = true;
	}
}

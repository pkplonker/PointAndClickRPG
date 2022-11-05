 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //

 using System;
 using UnityEngine;
using UnityEditor;

    /// <summary>
    ///CharacterStatsEditor full description
    /// </summary>
public class CharacterStatsEditor : MonoBehaviour
{
	[MenuItem("Custom/Player/Recover Run Energy")]
	static  void RecoverRunEnergy()
	{
		var runManager = FindObjectOfType<RunController>();
		if (runManager == null) return;

		runManager.RecoverRun(Single.MaxValue);
	}
}

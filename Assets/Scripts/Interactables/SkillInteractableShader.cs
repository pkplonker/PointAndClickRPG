//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System.Collections.Generic;
using Interactables;
using UnityEngine;

/// <summary>
///SkillInteractableShader full description
/// </summary>
public class SkillInteractableShader : SkillInteractable
{
	[SerializeField] private Material material;
[SerializeField] private string propertyName="_DECALEMISSIONONOFF";


	protected override void SetVisualsToFull()
	{
		if (material == null) return;
		Debug.Log("Set visuals to full");
		material.SetFloat(propertyName, 1);
	
	}

	protected override void SetVisualsToEmpty()
	{
		if (material == null) return;

		Debug.Log("Set visuals to empty");
		material.SetFloat(propertyName, 0);

	}
}
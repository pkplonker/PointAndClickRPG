using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	[SerializeField] public float interactionRadius { get; private set; } = 3f;
	private float interactionRangeSqr;
	protected PlayerWorldClickController targetter;
	private bool interactedWith = false;
	private void Awake()
	{
		interactionRangeSqr = interactionRadius * interactionRadius;
	}

	public virtual void Interact()
	{
		if (interactedWith) return;
		Debug.Log("Interacting with " + transform.name);
		interactedWith = true;
	}

	public virtual void Focus(PlayerWorldClickController player)
	{
		targetter = player;
		interactedWith = false;

	}

	public virtual void Defocus(PlayerWorldClickController player)
	{
		targetter = null;
		interactedWith = false;
	}
	
	
	public double GetInteractionRange() => interactionRangeSqr;
}
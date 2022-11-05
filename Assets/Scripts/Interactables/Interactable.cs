using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	[field:SerializeField] public float interactionRadius { get; private set; } = 2f;
	private float interactionRangeSqr;
	protected PlayerWorldClickController targetter;
	protected bool interactedWith = false;
	private void Awake()
	{
		interactionRangeSqr = interactionRadius * interactionRadius;
	}

	public virtual bool Interact(Stats stats)
	{
		if (interactedWith) return false;
		Debug.Log("Interacting with " + transform.name);
		interactedWith = true;
		return true;
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

	public float GetInteractionRadius() => interactionRadius;
}

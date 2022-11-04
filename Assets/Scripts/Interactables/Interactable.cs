using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	[SerializeField] private float interactionRadius = 3f;
	public virtual void Interact()
	{
		Debug.Log("Interacting with " + transform.name);
	}
}

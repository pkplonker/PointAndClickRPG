//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
	/// <summary>
	///CharacterMovement full description
	/// </summary>
	public class PlayerWorldClickController : MonoBehaviour
	{
		[SerializeField] private LayerMask interactableMask;
		[SerializeField] private float maxClickDistance = 100f;
		private InputHandler inputHandler;
		private Camera playerCamera;
		private RaycastHit hit;
		private Ray ray;
		private Locomotion locomotion;
		private Interactable currentInteractable;

		private void Start()
		{
			inputHandler = InputHandler.Instance;
			playerCamera = FindObjectOfType<Camera>();
			locomotion = GetComponent<Locomotion>();
			if (inputHandler == null || playerCamera == null || locomotion == null)
				Debug.LogError("PlayerWorldClickController: Missing required component");
		}

		private void Update()
		{
			if (!inputHandler.leftClick || EventSystem.current.IsPointerOverGameObject())
			{
				if (currentInteractable != null &&
				    (transform.position - currentInteractable.transform.position).sqrMagnitude <=
				    currentInteractable.GetInteractionRange())
				{
					Interact();
				}
				else locomotion.Move(hit.point);
			}
			else
			{
				ray = playerCamera.ScreenPointToRay(inputHandler.mousePosition);
				if (!Physics.Raycast(ray, out hit, maxClickDistance, interactableMask))
				{
					SetFocus(null);
					return;
				}
				if (hit.collider.TryGetComponent<Interactable>(out var interactable))
				{
					SetFocus(interactable);
					//if ((transform.position - currentInteractable.transform.position).sqrMagnitude <= currentInteractable.GetInteractionRange()) Interact();
					
				}
				else
				{
					SetFocus(null);
					locomotion.Move(hit.point);
				}
			}
		}

		private void SetFocus(Interactable interactable)
		{
			if (currentInteractable != null) currentInteractable.Defocus(this);
			currentInteractable = interactable;
			if(currentInteractable!= null) currentInteractable.Focus(this);
		}

		private void Interact()
		{
			locomotion.StopMovement();
			currentInteractable.Interact();
		}
	}
}
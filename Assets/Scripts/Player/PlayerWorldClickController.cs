//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
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
		private Stats stats;
		public static event Action<Vector3> OnMoveTargetSet;
		public static event Action<Vector3> OnClickedInteractable;
		private void Start()
		{
			stats = GetComponent<Stats>();
			inputHandler = InputHandler.instance;
			if(inputHandler==null) Debug.LogError("No InputHandler found");
			playerCamera = FindObjectOfType<Camera>();
			locomotion = GetComponent<Locomotion>();
			if (inputHandler == null || playerCamera == null || locomotion == null)
				Debug.LogError("PlayerWorldClickController: Missing required component");
		}

		private void Awake()
		{
			
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
				}  else if (currentInteractable!=null)
				{
					locomotion.Move(currentInteractable.transform.position);
				}
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
					MoveToNavigationPoint();
				}
			}
		}

		private void MoveToNavigationPoint()
		{
			SetFocus(null);
			locomotion.Move(hit.point);
			OnMoveTargetSet?.Invoke(hit.point);
		}

		private void SetFocus(Interactable interactable)
		{
			if (currentInteractable != null) currentInteractable.Defocus(this);
			currentInteractable = interactable;
			if (currentInteractable != null)
			{
				currentInteractable.Focus(this);
				OnClickedInteractable?.Invoke(interactable.transform.position);
				locomotion.SetManualRotation(currentInteractable);
			}else locomotion.SetAutoRotation();
			

		}

		private void Interact()
		{
			locomotion.StopMovement();
			currentInteractable.Interact(stats);
		}
	}
}
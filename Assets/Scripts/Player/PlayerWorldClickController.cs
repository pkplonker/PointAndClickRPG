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

		private void Start()
		{
			inputHandler = InputHandler.Instance;
			playerCamera = FindObjectOfType<Camera>();
			locomotion = GetComponent<Locomotion>();
			if(inputHandler==null || playerCamera== null || locomotion == null) Debug.LogError("PlayerWorldClickController: Missing required component");
		
		}

		private void Update()
		{
			if (!inputHandler.leftClick || EventSystem.current.IsPointerOverGameObject()) return;
			ray = playerCamera.ScreenPointToRay(inputHandler.mousePosition);
			if (!Physics.Raycast(ray, out hit, maxClickDistance, interactableMask)) return;
			if (hit.collider.TryGetComponent<Interactable>(out var interactable))
			{
				interactable.Interact();
			}
			else locomotion.SetDestination(hit.point);
		
		}
	}
}
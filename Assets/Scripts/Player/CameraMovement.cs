 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //

 using System;
 using UnityEngine;

    /// <summary>
    ///CameraMovement full description
    /// </summary>
    
public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Vector3 offset;
	[SerializeField] private Transform target;
	[Header("Zoom")]
	[SerializeField] private float zoomSpeed;
	[SerializeField] private float minZoom;
	[SerializeField] private float maxZoom;
	[SerializeField] private float moveSpeed;
	private float currentCameraZoom;
	private float yaw;
	private Camera playerCamera;
	private float pitch = 3f;
	private InputHandler inputHandler;
	private void Awake()
	{
		playerCamera = GetComponentInChildren<Camera>();
		if(!playerCamera) Debug.LogError("Camera not found");
		currentCameraZoom =  (maxZoom-minZoom)/2;
		inputHandler = InputHandler.instance;
	}

	private void Update()
	{
		ChangeScroll();
		HandleCameraPan();
	}

	private void HandleCameraPan()
	{
		if (!inputHandler.middleClick) return;
		yaw += inputHandler.mouseX * moveSpeed * Time.deltaTime;
	}

	private void ChangeScroll()
	{
		var amount = inputHandler.mouseScroll;
		currentCameraZoom -= amount * zoomSpeed * Time.deltaTime;
		currentCameraZoom = Mathf.Clamp(currentCameraZoom, minZoom, maxZoom);
		
	}

	private void LateUpdate()
	{
		Vector3 targetPosition = target.position ;
		transform.position = targetPosition - offset * currentCameraZoom;
		transform.LookAt(targetPosition+Vector3.up*pitch);
		transform.RotateAround(targetPosition,Vector3.up, yaw);
	}
}

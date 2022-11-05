//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using StuartHeathTools;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///InputHandler full description
/// </summary>
public class InputHandler : Singleton<InputHandler>
{
	private CharacterInput characterInput;
	
	public bool leftClick { get; private set; }
	public bool rightClick { get; private set; }
	public bool middleClick { get; private set; }
	public Vector2 mousePosition { get; private set; }
	public float mouseX { get; private set; }
	public float mouseScroll { get; private set; }
	public bool esc { get; private set; }
	public bool keyI { get; private set; }
	public bool shiftKey { get; private set; }
	private void OnDisable()
	{
		characterInput.Disable();
	}


	private void OnEnable()
	{
		characterInput = new CharacterInput();
		characterInput.Enable();
		characterInput.Default.LeftClick.performed += _ => leftClick = true;
		characterInput.Default.RightClick.performed += _ => rightClick = true;
		characterInput.Default.MiddleClick.started += _ => middleClick = true;
		characterInput.Default.MiddleClick.canceled += _ => middleClick = false;
		characterInput.Default.MouseScroll.performed += _ => mouseScroll = _.ReadValue<float>();
		characterInput.Default.MouseX.performed += _ => mouseX =_.ReadValue<float>();
		characterInput.Default.MousePosition.performed += _ => mousePosition = _.ReadValue<Vector2>();
		characterInput.Default.I.performed += _ => keyI = true;
		characterInput.Default.Esc.performed += _ => esc = true;
		characterInput.Default.ShiftClick.started += _ => shiftKey = true;
		characterInput.Default.ShiftClick.canceled += _ => shiftKey = false;

	}


	private void LateUpdate()
	{
		leftClick = false;
		rightClick = false;
		keyI = false;
		esc = false;
		mouseScroll = 0;
	}
}
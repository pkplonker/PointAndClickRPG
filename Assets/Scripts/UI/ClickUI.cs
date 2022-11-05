using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class ClickUI : MonoBehaviour
{
	private Canvas canvas;
	private Coroutine cor;
	[SerializeField] private float showTime = 1f;
	private WaitForSeconds wait;
	[SerializeField] private Color moveColor;
	[SerializeField] private Color interactColor;
	private Image image;

	private void Awake()
	{
		canvas = GetComponent<Canvas>();
		canvas.renderMode = RenderMode.WorldSpace;
		canvas.worldCamera = Camera.main;
		image = canvas.GetComponentInChildren<Image>();
		if (image == null) Debug.LogError("No sprite found");
		Hide();
		wait= new WaitForSeconds(showTime);
	}

	private void OnEnable()
	{
		PlayerWorldClickController.OnClickedInteractable += OnClickedInteractable;
		PlayerWorldClickController.OnMoveTargetSet += OnMoveTargetSet;
	}


	private void OnDisable()
	{
		PlayerWorldClickController.OnClickedInteractable -= OnClickedInteractable;
		PlayerWorldClickController.OnMoveTargetSet -= OnMoveTargetSet;
	}


	private void OnMoveTargetSet(Vector3 position)
	{
		Debug.Log("showing move ui");
		image.color = moveColor;
		Show(position);
	}

	private void OnClickedInteractable(Vector3 position)
	{
		Debug.Log("showing interact ui");
		image.color = interactColor;
		Show(position);
	}

	private void Show(Vector3 position)
	{
		if (cor != null) StopCoroutine(cor);
		cor = StartCoroutine(ShowCoroutine(position));
	}

	private IEnumerator ShowCoroutine(Vector3 position)
	{
		canvas.transform.position = position;
		canvas.enabled = true;
		yield return wait;
		Hide();
		cor = null;
	}

	private void Hide() => canvas.enabled = false;
}
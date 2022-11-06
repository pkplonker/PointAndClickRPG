using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(CanvasGroup))]

	public class ClickUI : MonoBehaviour
	{
		private Canvas canvas;
		private Coroutine cor;
		[SerializeField] private float showTime = 1f;
		private WaitForSeconds wait;
		[SerializeField] private Color moveColor;
		[SerializeField] private Color interactColor;
		private Image image;
		private Camera playerCamera;
		private CanvasGroup canvasGroup;
		private float timer = 0f;
		private void Awake()
		{
			canvas = GetComponent<Canvas>();
			canvas.renderMode = RenderMode.WorldSpace;
			playerCamera= Camera.main;
			canvas.worldCamera = playerCamera;
			image = canvas.GetComponentInChildren<Image>();
			if (image == null) Debug.LogError("No sprite found");
			Hide();
			wait= new WaitForSeconds(showTime);
			canvasGroup = GetComponent<CanvasGroup>();
			if(canvasGroup==null) Debug.LogError("No canvas group found");
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
			image.color = moveColor;
			Show(position);
		}

		private void OnClickedInteractable(Vector3 position)
		{
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
			Reset();

			canvas.transform.position = position;
			canvas.enabled = true;
		
			while (timer<=showTime)
			{
				timer += Time.deltaTime;
				canvasGroup.alpha=Mathf.Lerp(1,0,timer/showTime);

				yield return null;
			}

			canvasGroup.alpha = 0;
			Hide();
			cor = null;
		}

		private void Reset()
		{
			timer = 0f;
			canvasGroup.alpha = 1f;
		}

		private void LateUpdate()
		{
			transform.LookAt(playerCamera.transform);
		}



		private void Hide() => canvas.enabled = false;
	}
}
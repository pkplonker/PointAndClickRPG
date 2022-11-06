using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// tool tip system implemented with help from https://www.youtube.com/watch?v=HXFoUGw7eKk
namespace Stuart.UI
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasGroup))]
	public class Tooltip : MonoBehaviour
	{
		
		[SerializeField] private TextMeshProUGUI header;
		[SerializeField] private TextMeshProUGUI content;
		[SerializeField] private LayoutElement layoutElement;
		[SerializeField] private int characterWrapLimit;
		private RectTransform rectTransform;
		private CanvasGroup canvasGroup;

		[Header("Tweening")]
		[SerializeField] private float popDuration = 0.3f;
		[SerializeField] private Ease popEase = Ease.OutFlash;
		[SerializeField] private Vector2 mouseOffset = new (0,10);

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			canvasGroup = GetComponent<CanvasGroup>();
		}

	

		private void Update()
		{
			if (Application.isEditor)
			{
				var headerLength = header.text.Length;
				var contentLength = content.text.Length;
				layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
			}

		}

		public  void Show(string contentText,string headerText)
		{
			SetText(contentText, headerText);
			SetSize();
			var position = SetPosition(out var pivX, out var pivY);
			UpdatePivot(pivX, pivY, position);
			SetTween();
		}

		private void SetTween()
		{
			canvasGroup.alpha = 1f;
			rectTransform.DOScale(Vector3.one, popDuration).SetEase(popEase);
			canvasGroup.DOFade(1, popDuration / 2);
		}

		private void UpdatePivot(float pivX, float pivY, Vector2 position)
		{
			if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
			rectTransform.pivot = new Vector2(pivX, pivY);
			transform.position = position+mouseOffset;
		}

		private static Vector2 SetPosition(out float pivX, out float pivY)
		{
			Vector2 position = Input.mousePosition;
			pivX = position.x / Screen.width;
			pivY = position.y / Screen.height;
			return position;
		}

		private void SetSize()
		{
			var headerLength = header.text.Length;
			var contentLength = content.text.Length;
			layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
		}

		private void SetText(string contentText, string headerText)
		{
			if (string.IsNullOrEmpty(headerText)) header.gameObject.SetActive(false);
			else
			{
				header.gameObject.SetActive(true);
				header.text = headerText;
			}

			if (string.IsNullOrEmpty(contentText)) content.gameObject.SetActive(false);
			else
			{
				content.gameObject.SetActive(true);
				content.text = contentText;
			}
		}


		public void Hide()
		{
			canvasGroup.alpha = 0f;
			rectTransform.DOScale(Vector3.zero, popDuration).SetEase(popEase);
			canvasGroup.DOFade(0, popDuration);
		}
		public void HideInstant()
		{
			canvasGroup.alpha = 0f;
			
		}
	}
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{
	public class UISlotClickHandler : MonoBehaviour,IPointerClickHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			
		}

		private void HandleLeftClick()
		{
		}

		private void HandleRightClick()
		{
		}

	

		public void OnPointerClick(PointerEventData eventData)
		{
			
				switch (eventData.button)
				{
					case PointerEventData.InputButton.Left:
						HandleLeftClick();
						break;
					case PointerEventData.InputButton.Right:
						HandleRightClick();
						break;
				}
		}
	}
}
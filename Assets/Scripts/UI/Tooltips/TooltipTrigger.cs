using UnityEngine;
using UnityEngine.EventSystems;
// tool tip system implemented with help from https://www.youtube.com/watch?v=HXFoUGw7eKk

namespace Stuart.UI
{
	public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private string tooltipHeader;
		[SerializeField] private string tooltipContent;
		public void OnPointerEnter(PointerEventData eventData)=>TooltipSystem.instance.Show(tooltipContent,tooltipHeader);
		public void OnPointerExit(PointerEventData eventData)=>TooltipSystem.instance.Hide();
		
	}
}

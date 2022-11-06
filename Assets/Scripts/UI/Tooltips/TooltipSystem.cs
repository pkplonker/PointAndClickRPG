using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

// tool tip system implemented with help from https://www.youtube.com/watch?v=HXFoUGw7eKk

namespace Stuart.UI
{
   public class TooltipSystem : Singleton<TooltipSystem>
   {
      public Tooltip tooltip;
      private RectTransform tooltipRect;
      [SerializeField] private float showDelay = 0.5f;
      private Coroutine cor;
      private WaitForSeconds wait;
      protected override void Awake()
      {
         base.Awake();
         wait = new WaitForSeconds(showDelay);
         tooltipRect = tooltip.GetComponent<RectTransform>();
      }

      private void Start()=>         tooltip.HideInstant();


      public void Show(string content, string header ="")
      {
         if(cor!=null)StopCoroutine(cor);
         if (string.IsNullOrEmpty(content) && string.IsNullOrEmpty(header)) return;
         cor = StartCoroutine(ShowCor(content,header));
      }

      public void Hide() => tooltip.Hide();
   

      private IEnumerator ShowCor(string content, string header ="")
      {
         yield return wait;
         tooltip.Show(content,header);
      }
   }
}

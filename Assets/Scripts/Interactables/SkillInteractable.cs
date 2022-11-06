using System;
using System.Collections;
using UnityEngine;

namespace Interactables
{
	public class SkillInteractable : Interactable
	{
		private Stats stats;
		private Coroutine cor;
		[SerializeField] private string animationTriggerName;

		private void OnValidate()
		{
			if(string.IsNullOrEmpty(animationTriggerName))Debug.LogWarning("Animation cannot be blank");
		}

		public override bool Interact(Stats stats)
		{
			if (interactedWith) return false; 
			interactedWith = true;
			this.stats = stats;
			if(cor!= null) StopCoroutine(cor);
			cor = StartCoroutine(InteractionDelay());
			return true;
		}

		private IEnumerator InteractionDelay()
		{
			DoSkill();
			yield return new WaitForSeconds(4f);
			if(targetter!=null) cor = StartCoroutine(InteractionDelay());
		}

		private void DoSkill()
		{
			//do the skill stuff
			TriggerAnimation();
			Debug.Log("Doing a skill");
		}

		private void TriggerAnimation()
		{
		var animationController =	stats.GetComponentInChildren<PlayerAnimationController>();
			animationController.TriggerSkillAnimation(animationTriggerName);
		}
	}
}

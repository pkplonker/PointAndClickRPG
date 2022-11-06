using System;
using System.Collections;
using Items;
using UnityEngine;

namespace Interactables
{
	public class SkillInteractable : Interactable
	{
		private Stats stats;
		private Coroutine actionCoroutine;
		[SerializeField] private string animationTriggerName;
		[SerializeField] private ItemBase itemAward;
		[SerializeField] private int itemsToAwardPerAction;
		[SerializeField] private float respawnTime;
		[SerializeField] private int totalAmount;
		[SerializeField] private float experienceToAward;
		[SerializeField] private ParticleSystem particle;
		private int currentItemsAvailable;
		private Coroutine respawnCoroutine;
		
		protected override void Start()=>currentItemsAvailable = totalAmount;
		

		private void OnValidate()
		{
			if (string.IsNullOrEmpty(animationTriggerName)) Debug.LogWarning("Animation cannot be blank");
		}

		public override bool Interact(Stats stats)
		{
			if (interactedWith) return false;
			interactedWith = true;
			this.stats = stats;
			if (actionCoroutine != null) StopCoroutine(actionCoroutine);
			if(currentItemsAvailable> 0) actionCoroutine = StartCoroutine(InteractionDelay());
			return true;
		}

		private IEnumerator InteractionDelay()
		{
			DoSkill();
			yield return new WaitForSeconds(4f);
			if (targetter != null && currentItemsAvailable>0 ) actionCoroutine = StartCoroutine(InteractionDelay());
		}

		private void DoSkill()
		{
			//do the skill stuff
			TriggerAnimation();
			Debug.Log("Doing a skill");
		}

		private void TriggerAnimation()
		{
			var animationController = stats.GetComponentInChildren<PlayerAnimationController>();
			animationController.TriggerSkillAnimation(animationTriggerName,OnActionAnimationCompleteCallback);
		}

		private void OnActionAnimationCompleteCallback()
		{
			if (targetter == null) return;
			//todo award experience
			var amount = AwardItem();
			ReduceAvailableItemsLeft(amount);
		}

		private int AwardItem()
		{
			var inventory = stats.GetComponent<Inventory>();
			var amount = CalculateNumberToAward();
			if (inventory != null) inventory.Add(itemAward, amount);
			Debug.Log("Adding to invent from skill");
			return amount;
		}

		private IEnumerator RespawnCoroutine()
		{
			yield return new WaitForSeconds(respawnTime);
			IncreaseAvailableAmount();
			if(currentItemsAvailable < totalAmount)  respawnCoroutine= StartCoroutine(RespawnCoroutine());
		}

		private void IncreaseAvailableAmount()
		{
			currentItemsAvailable++;
			SetVisuals(false);
		}

		protected virtual void SetVisuals(bool isEmpty)
		{
			if (isEmpty) SetVisualsToEmpty();
			else SetVisualsToFull();
		}

		protected virtual void SetVisualsToFull()
		{
			Debug.Log("Set visuals to full");
		}

		protected virtual void SetVisualsToEmpty()
		{
			Debug.Log("Set visuals to empty");
		}

		private void ReduceAvailableItemsLeft(int amount)
		{
			currentItemsAvailable -= amount;
			respawnCoroutine ??= StartCoroutine(RespawnCoroutine());
			if (amount != 0) return;
			SetVisuals(true);
		}
		

		private int CalculateNumberToAward()
		{
			//todo calculate number to award - random?
			return itemsToAwardPerAction;
		}
	}
}
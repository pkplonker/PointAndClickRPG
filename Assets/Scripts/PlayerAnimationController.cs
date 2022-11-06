using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	private Animator animator;
	private Locomotion locomotion;
	private static readonly int Movement = Animator.StringToHash("Movement");

	private void Awake()
	{
		animator = GetComponent<Animator>();
		if (animator == null) Debug.LogError("Animator missing");
		locomotion = GetComponentInParent<Locomotion>();
		if (locomotion == null) Debug.LogError("locomotion missing");
	}

	private void OnEnable() => locomotion.SpeedChanged += CharacterSpeedChanged;

	private void OnDisable() => locomotion.SpeedChanged -= CharacterSpeedChanged;

	private void CharacterSpeedChanged(float newSpeed) => animator.SetFloat(Movement, newSpeed);

	public void TriggerSkillAnimation(string animationName)
	{
		animator.SetTrigger(animationName);
	}
}
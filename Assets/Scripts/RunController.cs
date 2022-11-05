 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //

 using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

    /// <summary>
    ///RunController full description
    /// </summary>
    
public class RunController : MonoBehaviour
{
  public event Action<float> OnRunEnergyChanged;
		private float currentRunEnergy;
		[SerializeField] private float maxRunEnergy;
		[SerializeField] private float refreshRate = 0.3f;
		[SerializeField] float currentDrainRate = 1f;
		[SerializeField] float currentRegenRate = 1f;
		private Locomotion locomotion;
		private bool isRunning;
		private InputHandler inputHandler;
		private void Awake()
		{
			currentRunEnergy = maxRunEnergy;
			OnRunEnergyChanged?.Invoke(currentRunEnergy);
		}

		private void Start()
		{
			inputHandler = InputHandler.instance;
			if (inputHandler == null) Debug.LogError("Input Handler not found");
			locomotion = GetComponent<Locomotion>();
			if (locomotion == null) Debug.LogError("missing locomotion");
			locomotion.SpeedChanged += CharacterSpeedChanged;
			StartCoroutine(RunningCor());
		}

		private void OnDisable()=>locomotion.SpeedChanged -= CharacterSpeedChanged;
		private void Update()=>RequestRun(inputHandler.shiftKey);
		private void CharacterSpeedChanged(float speed)
		{
			if (speed <= 0.01f) SetRunning(false);
		}

		private bool HasRunEnergy()=>currentRunEnergy > 0 + Time.deltaTime;
		
		IEnumerator RunningCor()
		{
			yield return new WaitForSeconds(refreshRate);
			if (isRunning) currentRunEnergy -= currentDrainRate * Time.deltaTime;
			else currentRunEnergy += currentRegenRate * Time.deltaTime;
			if (currentRunEnergy > maxRunEnergy) currentRunEnergy = maxRunEnergy;
			if (currentRunEnergy <= 0)
			{
				isRunning = false;
				currentRunEnergy = 0;
			}
			StartCoroutine(RunningCor());
		}


		private void SetRunning(bool isRunning)
		{
			this.isRunning = isRunning;
			locomotion.SetRun(isRunning);
		}

		public void RecoverRun(float amount)
		{
			currentRunEnergy += amount;
			if (currentRunEnergy > maxRunEnergy) currentRunEnergy = maxRunEnergy;
		}

		public void LoadState(object data)
		{
			OnRunEnergyChanged?.Invoke(currentRunEnergy);
		}

		public void RequestRun(bool isRequested)
		{
			if (HasRunEnergy() && isRequested) SetRunning(true);
			else SetRunning(false);
		}

}

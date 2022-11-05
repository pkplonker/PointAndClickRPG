 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //

 using UnityEngine;

 namespace Interactables
 {
	 /// <summary>
	 ///FireInteractable full description
	 /// </summary>
    
	 public class FireInteractable : Interactable
	 {
		 [SerializeField] private bool isFireActive = false;
		 [SerializeField] ParticleSystem fireParticles;
		 private void Start()
		 {
			 if(fireParticles == null)
			 {
				 Debug.LogWarning("FireInteractable: No fire particles assigned! - Trying to locate one in children");
				 fireParticles = GetComponentInChildren<ParticleSystem>();
				 if(fireParticles == null)
				 {
					 Debug.LogError("FireInteractable: No fire particles found in children!");
					 return;
				 }
			 }
			 fireParticles.gameObject.SetActive(isFireActive);
		 }

		 public override bool Interact(Stats stats)
		 {
			 if (base.Interact(stats) == false)
				 return false;
		
			 ToggleFire();
			 return true;
		 }
		 private void ToggleFire()
		 {
			 Debug.Log("Toggling fire");
			 isFireActive = !isFireActive;
			 fireParticles.gameObject.SetActive(isFireActive);
		 }

	 }
 }

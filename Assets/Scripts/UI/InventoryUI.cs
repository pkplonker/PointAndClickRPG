using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace UI
{
	public class InventoryUI : MonoBehaviour
	{
		[SerializeField] private GameObject inventorySlotPrefab;
		[SerializeField] private Transform slotContainer;
		private List<InventorySlotUI> inventorySlots = new();

		private Inventory inventory;


		private void Awake()
		{
		
		}

		private void Start()
		{
			
			inventory = PlayerManager.instance.GetComponent<Inventory>();
			if (inventory == null)
			{
				Debug.LogWarning("Missing inventory");
				return;
			}
			inventory.InventoryChanged += UpdateUI;

			SetUpSlots();

		}

		private void OnEnable()
		{
		}

	

		private void OnDisable()
		{
			if (inventory == null)
			{
				Debug.LogWarning("Missing inventory");
			}
			else
			{
				inventory.InventoryChanged -= UpdateUI;
			}
		}

	
		
		private void UpdateUI()
		{
			for (int i = 0; i < inventory.slots.Count; i++)
			{
				inventorySlots[i].UpdateUI(inventory.slots[i].item, inventory.slots[i].amount);
			}
		}


		private void SetUpSlots()
		{
			for (int i = 0; i < inventory.GetCapacity(); i++)
			{
				inventorySlots.Add(Instantiate(inventorySlotPrefab, slotContainer).GetComponentInChildren<InventorySlotUI>().Init(inventory, i));
			}
		}
	}
}
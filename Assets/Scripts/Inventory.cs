//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

/// <summary>
///Inventory full description
/// </summary>
public class Inventory : MonoBehaviour
{
	[field:SerializeField] public List<InventorySlot> slots { get; private set; }
	[SerializeField] private int capacity = 30;
	public event Action InventoryChanged;
	Dictionary<int, int> removeList = new();
	public int GetCapacity()=>capacity;

	public bool SetNewCapacity(int newCapacity)
	{
		if (capacity > newCapacity)
		{
			Debug.Log("Cannot make inventory smaller than current size");
			return false;
		}

		capacity = newCapacity;
		for (var i = 0; i < newCapacity - capacity; i++)
		{
			slots.Add(new InventorySlot());
		}

		return true;
	}

	private void Start()
	{
		slots = new List<InventorySlot>(capacity);
		for (var i = 0; i < capacity; i++)
		{
			slots.Add(new InventorySlot());
		}
	}

	/// <summary>
	/// Add items to inventory
	/// </summary>
	/// <param name="item">Item base Class</param>
	/// <param name="count">Number to store</param>
	/// <returns>how many of the request count that is not stored</returns>
	public int Add(ItemBase item, int count = 1)
	{
		if (item == null)
		{
			Debug.LogWarning("Trying to add null item to inventory");
			return count;
		}

		foreach (var t in slots)
		{
			//if slot is item and has capacity
			if (t.item != item || t.amount >= item.maxStack) continue;
			if (t.amount + count > item.maxStack)
			{
				t.amount = item.maxStack;
				count -= item.maxStack;
			}
			else
			{
				t.amount += count;
				count = 0;
				break;
			}
		}

		if (count != 0)
		{
			foreach (var t in slots)
			{
				if (count == 0) break;
				if (t.item != null) continue;
				var amount = count < item.maxStack ? count : item.maxStack;
				t.Add(item, amount);
				count -= amount;
			}
		}

		InventoryChanged?.Invoke();
		return count;
	}

	public bool Remove(ItemBase item, int count = 1)
	{
		removeList.Clear();
		if (item == null)
		{
			Debug.LogWarning("Trying to remove null item from inventory");
			return false;
		}

		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].item != item) continue;

			if (slots[i].amount > count)
			{
				removeList.Add(i, count);
				count = 0;
				break;
			}

			removeList.Add(i, slots[i].amount);
			count -= slots[i].amount;
		}

		if (count != 0) return false;
		foreach (var pair in removeList)
		{
			slots[pair.Key].Remove(pair.Value);
		}

		InventoryChanged?.Invoke();
		return true;
	}
	public void SwapSlots(int sender, int receiver)
	{
		if (slots[sender].item == slots[receiver].item)
		{
			if(slots[sender].amount + slots[receiver].amount > slots[sender].item.maxStack)
			{
				var temp = slots[sender].amount;
				slots[sender].amount = slots[sender].item.maxStack;
				slots[receiver].amount = temp - (slots[sender].item.maxStack - slots[receiver].amount);
			}
			else
			{
				slots[sender].amount += slots[receiver].amount;
				slots[receiver].amount = 0;
			}
				
		}
		else if (slots[receiver].item == null)
		{
			slots[receiver].Add(slots[sender].item, slots[sender].amount);
		}
		else
		{
			var temp = slots[sender].item;
			var tempAmount = slots[sender].amount;
			slots[sender].Add(slots[receiver].item, slots[receiver].amount);
			slots[receiver].Add(temp, tempAmount);
			
		}

		InventoryChanged?.Invoke();

	}
	public bool AddItemToSlot(ItemBase item, int quantity, int slotIndex)
	{
		if (slotIndex > slots.Count || slots[slotIndex] == null) return false;
		if (slots[slotIndex].item != null || item.maxStack < quantity) return false;
		slots[slotIndex].Add(item, quantity);
		InventoryChanged?.Invoke();

		return true;
	}

	public bool RemoveItemFromSlot(int slotIndex, int quantity)
	{
		if (slotIndex > slots.Count || slots[slotIndex] == null) return false;
		if (slots[slotIndex].item == null) return false;
		if (quantity > slots[slotIndex].amount) return false;
		slots[slotIndex].Remove(quantity);
		InventoryChanged?.Invoke();

		return true;
	}

}

[Serializable]
public class InventorySlot
{
	public ItemBase item;
	public int amount;

	public InventorySlot(ItemBase item, int amount)
	{
		this.item = item;
		this.amount = amount;
	}

	public InventorySlot()
	{
		item = null;
		amount = 0;
	}

	public void Add(ItemBase item, int amount)
	{
		if (this.item == item) this.amount += amount;
		else
		{
			this.item = item;
			this.amount = amount;
		}
	}

	public void Remove(int amount)
	{
		this.amount -= amount;
		if (this.amount < 0) Debug.LogWarning("Trying to remove more items than in slot");
		else
		{
			item = null;
			this.amount = 0;
		}
	}
}
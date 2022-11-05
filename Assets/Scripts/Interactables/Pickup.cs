using Items;
using UnityEngine;

namespace Interactables
{
	public class Pickup : Interactable
	{
		[field: SerializeField] public ItemBase item { get; private set; }

		public override bool Interact(Stats stats)
		{
			if (base.Interact(stats) == false)
				return false;
			if (stats.TryGetComponent<Inventory> (out var invent))
			{
				return PickupItem(invent);
			}
			Debug.LogWarning("No Inventory found on " + stats.gameObject.name);
			return false;
		}

		private bool PickupItem(Inventory inventory)
		{
			if (inventory == null) return false;
			if( inventory.Add(item))
			{
				Debug.Log("Picked up " + item.name);
				Destroy(gameObject);
				return true;
			}
			return false;
		}
	}
}

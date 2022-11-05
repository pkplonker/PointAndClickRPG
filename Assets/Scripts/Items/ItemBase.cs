//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using UnityEngine;

namespace Items
{
	/// <summary>
	///ItemBase full description
	/// </summary>
	public abstract class ItemBase : ScriptableObject
	{
		public string id = "";
		public string itemName = "";
		public string description = "";
		public Sprite sprite = null;
		public int maxStack = int.MaxValue;
		public GameObject prefab = null;

		public void Use(Inventory inventory)
		{
			throw new System.NotImplementedException();
		}
	}
}
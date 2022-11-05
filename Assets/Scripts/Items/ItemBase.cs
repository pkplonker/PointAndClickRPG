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
		public Sprite icon = null;
		public int maxStack = int.MaxValue;
		public bool isStackable = true;
		public GameObject prefab = null;
	}
}
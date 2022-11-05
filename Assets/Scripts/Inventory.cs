//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using Items;
using UnityEngine;

/// <summary>
///Inventory full description
/// </summary>
public class Inventory : MonoBehaviour
{
	public bool Add(ItemBase item)
	{
		Debug.Log("Added " + item.name);
		return true;
	}
}
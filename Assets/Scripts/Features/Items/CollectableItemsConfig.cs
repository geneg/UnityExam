using System;
using System.Collections.Generic;
using UnityEngine;


namespace Features.Items
{
	[CreateAssetMenu(fileName = "CollectableItemsConfig", menuName="Configurations/CollectableItemsConfig")]
	public class CollectableItemsConfig : ScriptableObject
	{
			public List<CollectableItemPair> _collectableItemsList;
		
			public GameObject GetCollectableItem(CollectableItemType key)
			{
				foreach (CollectableItemPair collectableItemPair in _collectableItemsList)
				{
					if (collectableItemPair.Key == key)
					{
						return collectableItemPair.Item;
					}
				}

				throw new InvalidOperationException($"Collectable Item for key {key} not found.");
			}
	}
	
	[Serializable]
	public struct CollectableItemPair
	{
		public CollectableItemType Key;
		public GameObject Item;
	}

	public enum CollectableItemType
	{
		Coin
	}
}

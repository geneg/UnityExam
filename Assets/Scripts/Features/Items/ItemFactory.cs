using System;
using UnityEngine;

namespace Features.Items
{
	public class ItemFactory
	{
		private readonly CollectableItemsConfig _itemsConfig;
		private readonly Transform _parent;
		public ItemFactory(CollectableItemsConfig itemsConfig, Transform parent)
		{
			_itemsConfig = itemsConfig;
			_parent = parent;
		}
		
		public ICollectableItem CreateItem(CollectableItemType type, Vector3 position)
		{
			GameObject template = _itemsConfig.GetCollectableItem(type);;
			
			switch (type)
			{
				case CollectableItemType.Coin:
					return new CoinCollectableItem(template, position, _parent);
				default:
					throw new InvalidOperationException("Invalid Item type");
			}
		}
	}
}

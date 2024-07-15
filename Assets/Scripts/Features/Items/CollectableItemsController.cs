using System;
using System.Collections.Generic;
using Common;
using Common.Services;
using Common.Utils;
using Data;
using Features.Items.Events;
using UnityEngine;

namespace Features.Items
{
	public class CollectableItemsController
	{
		private const int RandomAreaRadius = 50;

		public event Action OnItemCollected;
		
		private readonly CollectableItemsConfig _itemsConfig;
		private readonly LevelConfig _levelConfig;

		private List<ICollectableItem> _items = new List<ICollectableItem>();
		
		public CollectableItemsController(CollectableItemsConfig itemsConfig, LevelConfig levelConfig)
		{
			_itemsConfig = itemsConfig;
			_levelConfig = levelConfig;
			
			EventBroadcaster.Add<InitiateCollectableItemsEvent>(OnInitiateCollectableItems);
		}
		
		private void OnInitiateCollectableItems(InitiateCollectableItemsEvent e)
		{
			ItemFactory collectableItemsFactory = new ItemFactory(_itemsConfig, e.CollectablesContainer.transform);
			
			for (int i = 0; i < _levelConfig.itemsCount; i++)
			{
				Vector3 randomPos = RandomUtils.GetRandomPosition(Vector3.zero, RandomAreaRadius);
				ICollectableItem item = collectableItemsFactory.CreateItem(CollectableItemType.Coin, randomPos);
				item.OnCollected += OnItemCollectedHandler;
				
				_items.Add(item);
			}
		}
		
		private void OnItemCollectedHandler(ICollectableItem item)
		{
			EventBroadcaster.Broadcast(new ItemCollectedEvent());
			DisposeSingleItem(item);
		}

		private void DisposeSingleItem(ICollectableItem item)
		{
			item.OnCollected -= OnItemCollectedHandler;
			item.Dispose();
		}
		
		public void Dispose()
		{
			foreach (ICollectableItem item in _items)
			{
				DisposeSingleItem(item);
			}

			_items.Clear();
		}
	}
}

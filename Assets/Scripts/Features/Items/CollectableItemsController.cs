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
		private const int RandomAreaRadius = 10;

		public event Action OnItemCollected;
		
		private readonly CollectableItemsConfig _itemsConfig;
		private readonly LevelConfig _levelConfig;

		private List<ICollectableItem> _items = new List<ICollectableItem>();
		private readonly PlayerDataModel _playerData;
		
		public CollectableItemsController(CollectableItemsConfig itemsConfig, LevelConfig levelConfig, PlayerDataModel playerData)
		{
			_itemsConfig = itemsConfig;
			_levelConfig = levelConfig;

			_playerData = playerData;
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
			_playerData.SetItemCollected();	
			
			EventBroadcaster.Broadcast(new ItemCollectedEvent(_playerData.CollectedItems, _levelConfig.itemsCount));
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

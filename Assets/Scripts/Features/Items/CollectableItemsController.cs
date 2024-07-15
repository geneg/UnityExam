using Common;
using Common.Services;
using Common.Utils;
using Data;
using UnityEngine;

namespace Features.Items
{
	public class CollectableItemsController
	{
		private const int RandomAreaRadius = 50;
		
		private readonly CollectableItemsConfig _itemsConfig;
		private readonly LevelConfig _levelConfig;
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
				collectableItemsFactory.CreateItem(CollectableItemType.Coin, randomPos);
			}
			
			

		}
	}
}

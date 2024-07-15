using UnityEngine;

namespace Features.Items
{
	public class InitiateCollectableItemsEvent
	{
		public GameObject CollectablesContainer => _collectablesContainer;
		private readonly GameObject _collectablesContainer;
		
		public InitiateCollectableItemsEvent(GameObject collectablesContainer)
		{
			_collectablesContainer = collectablesContainer;
			
		}

	}
}

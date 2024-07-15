using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Items
{
	public class CoinCollectableItem : ICollectableItem 
	{
		private readonly float _itemGroundOffset = 0.7f; 
		private CoinView _newCoin;
		private GameObject _newCoinGameobject;
		public event Action<ICollectableItem> OnCollected;
		
		public CoinCollectableItem(GameObject template, Vector3 placeAt, Transform parent)
		{
			_newCoinGameobject =  Object.Instantiate(template, parent);
			_newCoin = _newCoinGameobject.GetComponent<CoinView>();

			_newCoin.OnTouch += OnItemTouch;
			
			Vector3 worldPosition = _newCoin.transform.TransformPoint(placeAt);
			float terrainHeight = Terrain.activeTerrain.SampleHeight(worldPosition) + _itemGroundOffset;
			worldPosition = new Vector3(worldPosition.x, terrainHeight, worldPosition.z);
			
			Vector3 localPositionWithHeight = _newCoin.transform.InverseTransformPoint(worldPosition);
			
			_newCoin.transform.localPosition = localPositionWithHeight;
		}
		
		private void OnItemTouch(Collider other)
		{
			OnCollected?.Invoke(this);
		}
		
		public void Dispose()
		{
			_newCoin.OnTouch -= OnItemTouch;
			Object.Destroy(_newCoinGameobject);
		}
	}
}

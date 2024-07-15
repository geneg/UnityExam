using UnityEngine;

namespace Features.Items
{
	public class CoinItem : IItem 
	{
		private readonly float _itemGroundOffset = 0.7f; 
		private CoinView _newCoin;
		private GameObject _newCoinGameobject;
		
		public CoinItem(GameObject template, Vector3 placeAt, Transform parent)
		{
			_newCoinGameobject =  Object.Instantiate(template, parent);
			_newCoin = _newCoinGameobject.GetComponent<CoinView>();
			
			Vector3 worldPosition = _newCoin.transform.TransformPoint(placeAt);
			float terrainHeight = Terrain.activeTerrain.SampleHeight(worldPosition) + _itemGroundOffset;
			worldPosition = new Vector3(worldPosition.x, terrainHeight, worldPosition.z);
			
			Vector3 localPositionWithHeight = _newCoin.transform.InverseTransformPoint(worldPosition);
			
			_newCoin.transform.localPosition = localPositionWithHeight;
		}

	}
}

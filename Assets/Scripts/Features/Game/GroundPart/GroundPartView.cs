using Features.Game.GroundPart.Character;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class GroundPartView : GamePartView
	{
		public CharacterView Character => _character;
		public GameObject CollectablesContainer => _collectablesContainer;
		public GroundGameUI GameUI => _gameUI;
		
		[SerializeField] private CharacterView _character;
		[SerializeField] private GameObject _collectablesContainer;
		[SerializeField] private GroundGameUI _gameUI;

	}
}

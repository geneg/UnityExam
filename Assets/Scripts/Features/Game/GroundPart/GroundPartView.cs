using Features.Game.GroundPart.Character;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class GroundPartView : GamePartView
	{
		public CharacterView Character => _character;
		public GameObject CollectablesContainer => _collectablesContainer;

		[SerializeField] private CharacterView _character;
		[SerializeField] private GameObject _collectablesContainer;
		
	}
}

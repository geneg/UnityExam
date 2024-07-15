using Features.Game.GroundPart.Character;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class GroundPartView : GamePartView
	{
		public CharacterView Character => _character;
		public GameObject CollectablesContainer => _collectablesContainer;
		public UIView UIView => _UIView;
		[SerializeField] private CharacterView _character;
		[SerializeField] private GameObject _collectablesContainer;
		[SerializeField] private UIView _UIView;

	}
}

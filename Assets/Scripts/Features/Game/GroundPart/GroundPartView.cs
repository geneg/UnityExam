using Features.Game.GroundPart.Character;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class GroundPartView : GamePartView
	{
		public CharacterView Character => _character;

		[SerializeField] private CharacterView _character;
	}
}

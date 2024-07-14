

using System;
using UnityEngine;


namespace Features.Game.FlightPart.Character
{
	public class CharacterJumpController
	{
		private readonly CharacterView _characterView;
		public event Action<Collision> OnCollide;
		
		public CharacterJumpController(CharacterView characterView)
		{
			_characterView = characterView;
			_characterView.OnCollide += OnCollideHandler;
		}
		
		private void OnCollideHandler(Collision other)
		{
			OnCollide?.Invoke(other);
		}

		public void SetSpawnPosition(Vector3 position)
		{
			_characterView.transform.position = position;
		}
		
		public void EnableCharacter()
		{
			_characterView.gameObject.SetActive(true);
		}
	}
}

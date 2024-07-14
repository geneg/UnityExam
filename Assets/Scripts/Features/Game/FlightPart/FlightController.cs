using System;
using System.Collections;
using Common.Utils;
using Features.Game.FlightPart.Character;
using Features.Game.FlightPart.Helicopter;
using UnityEngine;

namespace Features.Game.FlightPart
{
	public class FlightController : IGamePart
	{
		public event Action OnPartEnded;

		private readonly FlightPartView _view;
		private Vector3 _spawnPosition;
		private readonly HelicopterController _helicopterController;
		private readonly CharacterJumpController _characterJumpController;
		private readonly CameraDirector<Cameras> _cameraDirector;
		
		public FlightController(FlightPartView flightPartView)
		{
			_view = flightPartView;
			
			_helicopterController = new HelicopterController(_view.Helicopter, _view.GetSpawnPosition());
			_characterJumpController = new CharacterJumpController(_view.Character);

			_cameraDirector = new CameraDirector<Cameras>();
			_cameraDirector.AddCamera(Cameras.Helicopter, _view.Helicopter.Camera);
			_cameraDirector.AddCamera(Cameras.Character, _view.Character.Camera);
			
			_helicopterController.OnCanJump += OnCanJumpHandler;
			_view.InputController.OnJump += OnJumpHandler;
			_characterJumpController.OnCollide += OnCollideHandler;
			_cameraDirector.Show(Cameras.Helicopter);
		}
		
		private void OnCollideHandler(Collider other)
		{
			if (_view.GroundCollider == other)
			{
				OnPartEnded?.Invoke();
			}
		}

		private void OnJumpHandler()
		{
			Vector3 position = _view.Helicopter.transform.position;
			
			_helicopterController.FlyBack(position, _view.FlightDuration);
			_characterJumpController.SetSpawnPosition(position);
			_characterJumpController.EnableCharacter();
			
			_cameraDirector.Show(Cameras.Character);
		}

		private void OnCanJumpHandler(bool value)
		{
			_view.InputController.SetCanJump(value);
		}
		
		public void Play()
		{
			_helicopterController.FlyTo(_view.MapCenterPosition.position, _view.FlightDuration);
		}
		
		public void Clear()
		{
			_helicopterController.OnCanJump -= OnCanJumpHandler;
			_view.InputController.OnJump -= OnJumpHandler;
			_characterJumpController.OnCollide -= OnCollideHandler;
		}
	}

	public enum Cameras
	{
		Helicopter,
		Character,
	}
}

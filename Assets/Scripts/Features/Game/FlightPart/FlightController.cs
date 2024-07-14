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
		private readonly CameraDirector<FlyPartCameras> _cameraDirector;
		private SharedDataModel _sharedDataModel;
		
		public FlightController(FlightPartView flightPartView, SharedDataModel sharedDataModel)
		{
			_view = flightPartView;
			_sharedDataModel = sharedDataModel;
			
			_helicopterController = new HelicopterController(_view.Helicopter, _view.GetSpawnPosition());
			_characterJumpController = new CharacterJumpController(_view.Character);

			_cameraDirector = new CameraDirector<FlyPartCameras>();
			_cameraDirector.AddCamera(FlyPartCameras.Helicopter, _view.Helicopter.Camera);
			_cameraDirector.AddCamera(FlyPartCameras.Character, _view.Character.Camera);
			
			_helicopterController.OnCanJump += OnCanJumpHandler;
			_view.InputController.OnJump += OnJumpHandler;
			_characterJumpController.OnCollide += OnCollideHandler;
			_cameraDirector.Show(FlyPartCameras.Helicopter);
		}
		
		private void OnCollideHandler(Collision other)
		{
			if (_view.GroundCollider == other.collider)
			{
				Transform transform = _view.Character.transform;
				_sharedDataModel.CharacterPosition = transform.position;
				_sharedDataModel.CharacterRotation = transform.rotation;
					
				OnPartEnded?.Invoke();
			}
		}

		private void OnJumpHandler()
		{
			Vector3 position = _view.Helicopter.transform.position;
			
			_helicopterController.FlyBack(position, _view.FlightDuration);
			_characterJumpController.SetSpawnPosition(position);
			_characterJumpController.EnableCharacter();
			
			_cameraDirector.Show(FlyPartCameras.Character);
		}

		private void OnCanJumpHandler(bool value)
		{
			_view.InputController.SetCanJump(value);
		}
		
		public void Play()
		{
			_view.enabled = true;
			_view.gameObject.SetActive(true);
			
			_helicopterController.FlyTo(_view.MapCenterPosition.position, _view.FlightDuration);
		}
		
		public void Stop()
		{
			_view.enabled = false;
			_view.gameObject.SetActive(false);
		}

		public void Clear()
		{
			_cameraDirector.RemoveCamera(FlyPartCameras.Helicopter);
			_cameraDirector.RemoveCamera(FlyPartCameras.Character);
			
			_helicopterController.OnCanJump -= OnCanJumpHandler;
			_view.InputController.OnJump -= OnJumpHandler;
			_characterJumpController.OnCollide -= OnCollideHandler;
		}
	}

	public enum FlyPartCameras
	{
		Helicopter,
		Character,
	}
}

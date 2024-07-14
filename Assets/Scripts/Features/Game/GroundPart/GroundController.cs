using System;
using System.Collections;
using Cinemachine;
using Common.Utils;
using Features.Game.GroundPart.Character;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class GroundController : IGamePart
	{
		private GroundPartView _view;
		private readonly CameraDirector<GroundPartCameras> _cameraDirector;
		private SharedDataModel _sharedDataModel;
		private readonly CharacterGroundController _characterGroundController;

		public GroundController(GroundPartView groundPartView, SharedDataModel sharedDataModel)
		{
			_view = groundPartView;
			_sharedDataModel = sharedDataModel;

			_characterGroundController = new CharacterGroundController(_view.Character);
			
			_cameraDirector = new CameraDirector<GroundPartCameras>();
			_cameraDirector.AddCamera(GroundPartCameras.Character, _view.Character.Camera);
		}

		public event Action OnPartEnded;
		
		public void Play()
		{
			_view.enabled = true;
			_view.gameObject.SetActive(true);
			
			//set player position
			Transform transform = _view.Character.transform;
			
			_view.Character.CharacterController.enabled = false;
			transform.SetPositionAndRotation(_sharedDataModel.CharacterPosition + new Vector3(0,0.3f,0), transform.rotation = _sharedDataModel.CharacterRotation);
			_view.Character.CharacterController.enabled = true;
			
			_cameraDirector.Show(GroundPartCameras.Character);
		}
		public void Stop()
		{
			_view.enabled = false;
			_view.gameObject.SetActive(false);
		}

		public void Clear()
		{
			
		}

		public void Reset()
		{
			Debug.Log("reset ground");
		}

		private IEnumerator Dummy()
		{
			yield return new WaitForSeconds(3);
			OnPartEnded?.Invoke();
		}
	}
	
	public enum GroundPartCameras
	{
		Character
	}
}

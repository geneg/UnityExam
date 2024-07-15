using System;
using System.Collections;
using Common;
using Common.Services;
using Common.Utils;
using Data;
using Features.Game.GroundPart.Character;
using Features.Items.Events;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class GroundController : IGamePart
	{
		private GroundPartView _view;
		private readonly CameraDirector<GroundPartCameras> _cameraDirector;
		private SharedDataModel _sharedDataModel;
		private readonly CharacterGroundController _characterGroundController;
		private readonly AsyncTimer _timer;
		private readonly LevelConfig _levelConfig;

		public GroundController(GroundPartView groundPartView, SharedDataModel sharedDataModel, LevelConfig levelConfig)
		{
			_view = groundPartView;
			_sharedDataModel = sharedDataModel;
			_levelConfig = levelConfig;
			_characterGroundController = new CharacterGroundController(_view.Character);
			_timer = new AsyncTimer();
			_timer.OnTimerEnded += OnTimerEnded;
			_timer.OnSecondElapsed += OnTimerUpdate;
			
			_cameraDirector = new CameraDirector<GroundPartCameras>();
			_cameraDirector.AddCamera(GroundPartCameras.Character, _view.Character.Camera);

			EventBroadcaster.Add<ItemCollectedEvent>(OnItemCollected);
			_view.UIView.SetItemsText($"{0} of {levelConfig.itemsCount}");
		}
		
		private void OnItemCollected(ItemCollectedEvent e)
		{
			_view.UIView.SetItemsText($"{e.CurrentCount} of {e.Total}");
		}

		private void OnTimerEnded()
		{
			OnPartEnded?.Invoke();
		}
		
		private void OnTimerUpdate(int seconds)
		{
			_view.UIView.SetTimerText(seconds);
		}
		

		public event Action OnPartEnded;
		
		public void Play()
		{
			_view.enabled = true;
			_view.gameObject.SetActive(true);
			
			//set player position
			Transform transform = _view.Character.transform;
			
			_view.Character.CharacterController.enabled = false;
			transform.SetPositionAndRotation(_sharedDataModel.CharacterPosition + new Vector3(0,1f,0), transform.rotation = _sharedDataModel.CharacterRotation);
			_view.Character.CharacterController.enabled = true;
			_cameraDirector.Show(GroundPartCameras.Character);

			_timer.StartCountdownTimer(_levelConfig.duration);
				
			EventBroadcaster.Broadcast(new InitiateCollectableItemsEvent(_view.CollectablesContainer));

			
		}
		
		public void Stop()
		{
			_view.enabled = false;
			_view.gameObject.SetActive(false);
		}

		public void Clear()
		{
			_timer.StopTimer();
			_timer.OnTimerEnded -= OnTimerEnded;
			_timer.OnSecondElapsed -= OnTimerUpdate;

		}
	}
	
	public enum GroundPartCameras
	{
		Character
	}
}

using System;
using Common;
using Features.Lobby.Events;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Features.Lobby
{
	public class LobbyView : BaseView
	{
		[SerializeField]
		private Button _playButton;
		
		public event Action OnPlay;
		
		private void Start()
		{
			_playButton.onClick.AddListener(OnPlayClickHandler);
		}
		
		private void OnPlayClickHandler()
		{
			OnPlay?.Invoke();
		}
		
		private void OnDestroy()
		{
			_playButton.onClick.RemoveListener(OnPlayClickHandler);
		}
	}
}

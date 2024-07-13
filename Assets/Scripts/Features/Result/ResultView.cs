using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Result
{
	public class ResultView : BaseView
	{
		[SerializeField] private Button _returnButton;
		[SerializeField] private Button _playButton;
		
		public event Action OnReturn;
		public event Action OnPlay;
		
		private void Start()
		{
			_returnButton.onClick.AddListener(OnReturnClickHandler);
			_playButton.onClick.AddListener(OnPlayClickHandler);
		}
		
		private void OnPlayClickHandler()
		{
			OnPlay?.Invoke();
		}
		
		private void OnReturnClickHandler()
		{
			OnReturn?.Invoke();
		}
		
		private void OnDestroy()
		{
			_playButton.onClick.RemoveListener(OnPlayClickHandler);
			_returnButton.onClick.RemoveListener(OnReturnClickHandler);
		}
	}
}

using System;
using System.Collections.Generic;

namespace Features.Game
{
	public class GamePartsPlayer
	{
		private int _currentPartIndex = 0;
		private readonly List<IGamePart> _parts = new List<IGamePart>();
		private IGamePart _currentPart;
		
		public event Action OnSequenceEnd;
		
		public void AddGamePart(IGamePart gamePart)
		{
			_parts.Add(gamePart);
		}

		public void StartPlay()
		{
			PlayNextPart();
		}

		private void PlayNextPart()
		{
			if (_currentPart != null)
			{
				_currentPart.OnPartEnded -= OnPartEndHandler;
			}
			
			if (_currentPartIndex >= _parts.Count)
			{
				OnSequenceEnd?.Invoke();
				return;
			}
			
			_currentPart = _parts[_currentPartIndex];
			_currentPart.OnPartEnded += OnPartEndHandler;
			_currentPart.Play();
			_currentPartIndex++;
		}
		
		public void Clear()
		{
			_currentPartIndex = 0;
			foreach (IGamePart gamePart in _parts)
			{
				gamePart.Clear();
			}

			_parts.Clear();
		}
		
		private void OnPartEndHandler()
		{
			PlayNextPart();
		}
	}

	public interface IGamePart
	{
		event Action OnPartEnded;
		void Play();
		void Clear();
	}
}

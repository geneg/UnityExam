using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Features.Game
{
	public class GameView : BaseView
	{
		public CinemachineVirtualCamera CharacterCamera => _characterCamera;
		public float FpvDistance => _fpvDistance;

		[SerializeField] private CinemachineVirtualCamera _characterCamera;
		[SerializeField] private float _fpvDistance;

		[SerializeField] private List<GamePartView> _gameParts;
		
		public T GetPart<T>() where T : MonoBehaviour
		{
			foreach (GamePartView part in _gameParts)
			{
				if (part is T foundPart)
				{
					return foundPart;
				}
			}
        
			throw new InvalidOperationException($"No part of type {typeof(T).Name} found.");
		}
	}
}

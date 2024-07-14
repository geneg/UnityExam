using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class GroundController : IGamePart
	{
		private GameView _view;
		
		public GroundController(GroundPartView groundPartView)
		{
			
		}

		public event Action OnPartEnded;
		
		public void Play()
		{
			Debug.Log("play ground");
			
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
}

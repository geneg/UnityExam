using System;
using System.Collections;
using Common;
using UnityEngine;

namespace Features.Game
{
	public class FlightController : IGamePart {
		private readonly UnityHelper _unityHelper;
		public FlightController(UnityHelper unityHelper)
		{
			_unityHelper = unityHelper;
		}

		public event Action OnPartEnded;
		
		public void Play()
		{
			Debug.Log("play flight");
			_unityHelper.StartCoroutine(Dummy());
		}
		
		public void Reset()
		{
			Debug.Log("Reset flight");
		}
		
		private IEnumerator Dummy()
		{
			yield return new WaitForSeconds(3);
			OnPartEnded?.Invoke();
		}
	}
}

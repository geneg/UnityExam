using System;
using System.Collections;
using Common;
using UnityEngine;

namespace Features.Game
{
	public class GroundController : IGamePart {
		private readonly UnityHelper _unityHelper;

		public GroundController(UnityHelper unityHelper)
		{
			_unityHelper = unityHelper;
		}

		public event Action OnPartEnded;
		
		public void Play()
		{
			Debug.Log("play ground");
			_unityHelper.StartCoroutine(Dummy());
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

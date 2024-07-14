using System;
using UnityEngine;

namespace Features.Game.FlightPart
{
	public class FligtPartInputController : MonoBehaviour
	{
		private bool _canJump;
		public event Action OnJump;
		
		public void SetCanJump(bool value)
		{
			_canJump = value;
		}
		
		private void Update()
		{
			if (!_canJump) return;
			
			if (Input.GetKeyUp(KeyCode.Space))
			{
				OnJump?.Invoke();
				_canJump = false;
			}
		}
	}
}

using System;
using DG.Tweening;
using UnityEngine;

namespace Features.Game.FlightPart.Helicopter
{
	public class HelicopterController
	{
		private readonly Vector3 _startPos;
		private HelicopterView _helicopterView;

		public event Action<bool> OnCanJump;
		
		public HelicopterController(HelicopterView helicopterView, Vector3 startPos)
		{
			_startPos = startPos;
			_helicopterView = helicopterView;
		}
		
		public void FlyTo(Vector3 position, int viewFlightDuration)
		{
			Vector3 direction = (position - _startPos).normalized;
			
			_helicopterView.transform.position = _startPos;
			_helicopterView.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			_helicopterView.transform.DOMove(position, viewFlightDuration);

			_helicopterView.OnCanJump += OnCanJumpHandler;
		}
		
		private void OnCanJumpHandler(bool value)
		{
			OnCanJump?.Invoke(value);
		}

		public void FlyBack(Vector3 position, int viewFlightDuration)
		{
			Vector3 direction = (_startPos - position).normalized;
			
			_helicopterView.transform.position = position;
			_helicopterView.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			_helicopterView.transform.DOMove(_startPos, viewFlightDuration);
		}
	}
}

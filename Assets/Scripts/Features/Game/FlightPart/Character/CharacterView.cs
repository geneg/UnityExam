using System;
using Cinemachine;
using UnityEngine;

namespace Features.Game.FlightPart.Character
{
	public class CharacterView : MonoBehaviour
	{
		public CinemachineVirtualCamera Camera => _camera;
		[SerializeField] private CinemachineVirtualCamera _camera;

		public event Action<Collision> OnCollide;

		private void OnCollisionEnter(Collision other)
		{
			OnCollide?.Invoke(other);
		}
		
	}
}

using System;
using Cinemachine;
using UnityEngine;

namespace Features.Game.FlightPart.Character
{
	public class CharacterView : MonoBehaviour
	{
		public CinemachineVirtualCamera Camera => _camera;
		[SerializeField] private CinemachineVirtualCamera _camera;

		public event Action<Collider> OnCollide;

		private void OnTriggerEnter(Collider other)
		{
			OnCollide?.Invoke(other);
		}
	}
}

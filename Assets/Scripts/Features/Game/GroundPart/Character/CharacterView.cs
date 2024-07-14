using System;
using Cinemachine;
using UnityEngine;

namespace Features.Game.GroundPart.Character
{
	public class CharacterView : MonoBehaviour
	{
		public CinemachineVirtualCamera Camera => _camera;
		[SerializeField] private CinemachineVirtualCamera _camera;

		public CharacterController CharacterController => _characterController;
		[SerializeField] private CharacterController _characterController;
		
		public event Action<Collider> OnCollide;

		private void OnTriggerEnter(Collider other)
		{
			OnCollide?.Invoke(other);
		}
		
	}
}

using System;
using Cinemachine;
using UnityEngine;

namespace Features.Game.FlightPart.Helicopter
{
    public class HelicopterView : MonoBehaviour
    {
        public CinemachineVirtualCamera Camera => _camera;
        [SerializeField] private CinemachineVirtualCamera _camera;
        
        public event Action<bool> OnCanJump;
        private void OnTriggerEnter(Collider other)
        {
            OnCanJump?.Invoke(true);
        }
    
        private void OnTriggerExit(Collider other)
        {
            OnCanJump?.Invoke(false);
        }
    }
}

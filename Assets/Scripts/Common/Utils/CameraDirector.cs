using System;
using System.Collections.Generic;
using Cinemachine;

namespace Common.Utils
{
	public class CameraDirector<TKey>
	{
		private Dictionary<TKey, CinemachineVirtualCamera> _cameras = new Dictionary<TKey, CinemachineVirtualCamera>();

		private const int InactivePriority = 0;
		private const int ActivePriority = 10;

		public void AddCamera(TKey key, CinemachineVirtualCamera camera)
		{
			if (!_cameras.ContainsKey(key))
			{
				_cameras.Add(key, camera);
			}
		}

		public void RemoveCamera(TKey key)
		{
			if (_cameras.ContainsKey(key))
			{
				_cameras.Remove(key);
			}
		}

		// Switches to the specified camera by changing its priority
		public void Show(TKey key)
		{
			if (_cameras.ContainsKey(key))
			{
				foreach (CinemachineVirtualCamera camera in _cameras.Values)
				{
					camera.Priority = InactivePriority;
				}

				_cameras[key].Priority = ActivePriority;
			}
			else
			{
				throw new InvalidOperationException($"Camera with key {key} not found.");
			}
		}
	}
}

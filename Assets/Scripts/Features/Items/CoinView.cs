using System;
using UnityEngine;

namespace Features.Items
{
	public class CoinView : MonoBehaviour
	{
		public event Action<Collider> OnTouch;
		private void OnTriggerEnter(Collider other)
		{
			OnTouch?.Invoke(other);
		}
	}
}

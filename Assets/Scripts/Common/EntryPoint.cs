using UnityEngine;

namespace Common
{
	/// <summary>
	/// Single entry point for whole app.
	/// </summary>
	public abstract class EntryPoint : MonoBehaviour
	{
		protected MonoBehaviour UnityHelper => this;
		
		private void Awake()
		{
			OnAwake();
			DontDestroyOnLoad(this.gameObject);
		}

		private void Start()
		{
			OnStart();
		}
		
		private void OnDestroy()
		{
			OnAppDestroy();
		}

		protected abstract void OnAwake();
		protected abstract void OnStart();
		protected abstract void OnAppDestroy();
	}
}

using UnityEngine;

namespace Common
{
	/// <summary>
	/// Single entry point for whole app.
	/// </summary>
	[RequireComponent(typeof(UnityHelper))]
	public abstract class EntryPoint : MonoBehaviour
	{
		protected UnityHelper UnityHelper => _unityHelper;
		private UnityHelper _unityHelper;
		
		private void Awake()
		{
			_unityHelper = FindObjectOfType<UnityHelper>();
			
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

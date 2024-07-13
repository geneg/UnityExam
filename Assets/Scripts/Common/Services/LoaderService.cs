using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Services
{
	public class LoaderService : IService
	{
		private readonly UnityHelper _unityHelper;
		
		public event Action OnLoadComplete;
		
		public LoaderService(UnityHelper unityHelper)
		{
			_unityHelper = unityHelper;
		}
		
		public void LoadScene(string sceneId)
		{
			_unityHelper.StartCoroutine(InnerLoad(sceneId));
		}

		private IEnumerator InnerLoad(string sceneId)
		{
			AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneId);

			while (!loadOp.isDone)
			{
				yield return null;
			}
			
			OnLoadComplete?.Invoke();
		}
	}
}

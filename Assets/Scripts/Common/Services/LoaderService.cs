using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Services
{
	public class LoaderService : IService
	{
		private readonly MonoBehaviour _unityHelper;
		
		public LoaderService(MonoBehaviour unityHelper)
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
		}
	}
}

using UnityEngine;

namespace Common
{
	public class UnityHelper : MonoBehaviour
	{
		public bool TryFindObjectOfType<T>(out T foundObject) where T : Object
		{
			foundObject = GameObject.FindObjectOfType<T>();
			return foundObject != null;
		}
		
		//public void StartCor(IEnumerator coroutine)
		//{
		//	StartCoroutine(coroutine);
		//}
	}
}

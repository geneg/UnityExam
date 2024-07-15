using UnityEngine;

namespace Common.Utils
{
	public static class RandomUtils
	{
		public static Vector3 GetRandomPosition(Vector3 center, float radius)
		{
			float angle = Random.Range(0f, Mathf.PI * 2);
			float distance = Random.Range(0f, radius);

			float x = center.x + distance * Mathf.Cos(angle);
			float z = center.z + distance * Mathf.Sin(angle);
			
			return new Vector3(x, center.y, z);
		}
	}
}

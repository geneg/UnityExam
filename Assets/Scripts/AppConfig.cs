using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AppConfig", menuName="Configurations/AppConfig")]
	
public class AppConfig : ScriptableObject
{
	[Serializable]
	public struct SceneConfig
	{
		public SceneKey key;
		public string name;
	}

	public List<SceneConfig> sceneConfigs;
		
	public string GetSceneName(SceneKey key)
	{
		foreach (var config in sceneConfigs)
		{
			if (config.key == key)
			{
				return config.name;
			}
		}

		throw new Exception($"Scene name for key {key} not found.");
	}
}
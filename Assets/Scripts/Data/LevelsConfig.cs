using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "LevelsConfig", menuName="Configurations/LevelsConfig")]
	public class LevelsConfig : ScriptableObject
	{
		[SerializeField] private List<LevelConfig> levelConfig;
		
		public LevelConfig GetLevelConfig(int idx)
		{
			return levelConfig[idx];
		}
	}
	
	[Serializable]
	public struct LevelConfig
	{
		public int duration;
		public int itemsCount;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Services
{
	public class ConfigService : IService
	{
		private const string configFolder = "Configs";
		private readonly Dictionary<Type, ScriptableObject> _configCache = new Dictionary<Type, ScriptableObject>();

		public T GetConfig<T>() where T : ScriptableObject
		{
			Type type = typeof(T);

			if (_configCache.ContainsKey(type)) return _configCache[type] as T;
			
			T config = Resources.Load<T>($"{configFolder}/{type.Name}");
			if (config == null)
			{
				throw new Exception($"Configuration of type {type.Name} not found in Resources.");
			}
			
			_configCache[type] = config;
			return _configCache[type] as T;
		}
	}
}

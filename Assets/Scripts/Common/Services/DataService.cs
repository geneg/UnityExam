using System;
using System.Collections.Generic;

namespace Common.Services
{
	public class DataService : IService
	{
		private readonly Dictionary<Type, DataModel> _models = new Dictionary<Type, DataModel>();

		public T Get<T>() where T : DataModel, new()
		{
			Type type = typeof(T);

			if (_models.ContainsKey(type)) return _models[type] as T;
			
			T model = new T();
			_models[type] = model;
			
			return model;
		}
	}
}

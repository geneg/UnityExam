using System;
using System.Collections.Generic;

namespace Common
{
	public class ServiceResolver
	{
		private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
		
		public ServiceResolver()
		{
			
		}
		
		public void Register<T>(T service) where T : IService
		{
			if (!_services.TryAdd(typeof(T), service))
			{
				throw new InvalidOperationException("Service of type " + typeof(T) + " already registred.");
			}
		}

		public T Get<T>() where T : class, IService
		{
			if (_services.TryGetValue(typeof(T), out IService service))
			{
				return service as T;
			}
			
			throw new InvalidOperationException("Service of type " + typeof(T) + " not found.");
		}
	}
	
	public interface IService
	{
			
	}
}

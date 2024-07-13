using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	public static class EventBroadcaster
	{
			private static readonly Dictionary<Type, List<object>> _eventsDictionary = new Dictionary<Type, List<object>>();

			public static void Add<T>(Action<T> action)
			{
				if (!_eventsDictionary.ContainsKey(typeof(T)))
					_eventsDictionary.Add(typeof(T), new List<object>());
				_eventsDictionary[typeof(T)].Add(action);
			}

			public static void Remove<T>(Action<T> action)
			{
				if (_eventsDictionary.ContainsKey(typeof(T)))
					_eventsDictionary[typeof(T)].Remove(action);
			}

			public static void Broadcast<T>(T arg)
			{
				if (!_eventsDictionary.ContainsKey(typeof(T)))
					return;

				List<object> actions = _eventsDictionary[typeof(T)];
				for (int i = 0; i < actions.Count; i++)
				{
					try
					{
						Action<T> currentAction = (Action<T>)actions[i];
						currentAction.Invoke(arg);
					}
					catch (Exception e)
					{
						Debug.LogException(e);
					}
				}
			}
	}
}

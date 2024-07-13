using System;
using System.Collections.Generic;
using System.Reflection;

namespace Common
{
	public class StateMachine
	{
		private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
		private readonly ServiceResolver _serviceResolver;
		private IState _currentState;

		public UnityHelper UnityHelper { get; }

		public StateMachine(ServiceResolver serviceResolver, UnityHelper unityHelper)
		{
			_serviceResolver = serviceResolver;
			UnityHelper = unityHelper;
		}

		public void AddState<T>() where T : IState
		{
			Type type = typeof(T);
		
			ConstructorInfo constructor = type.GetConstructor(new[] { typeof(StateMachine), typeof(ServiceResolver)});
			if (constructor == null)
			{
				throw new InvalidOperationException($"Type {type.FullName} does not have a constructor with a parameters of type {typeof(StateMachine).FullName} {typeof(ServiceResolver).FullName}");
			}
		
			T state = (T) constructor.Invoke(new object[] { this, _serviceResolver });
			_states.Add(type, state);
		}
		
		public void SetDefaultState<T>() where T : IState
		{
			_currentState = _states[typeof(T)];
			_currentState.OnEnterState();
		}

		public void ChangeState<T>() where T : IState
		{
			_currentState.OnExitState();
			_currentState = _states[typeof(T)];
			_currentState.OnEnterState();
		}
	
	
	}

	public interface IState
	{
		void OnExitState();
		void OnEnterState();
	}
}

using Common;
using Common.Services;

namespace States
{
	public abstract class BaseState : IState
	{
		protected readonly StateMachine StateMachine;
		protected readonly LoaderService LoaderService;
		protected readonly AppConfig AppConfig;
		
		protected BaseState(StateMachine stateMachine, ServiceResolver serviceResolver)
		{
			StateMachine = stateMachine;
			LoaderService = serviceResolver.Get<LoaderService>();
			AppConfig = serviceResolver.Get<ConfigService>().GetConfig<AppConfig>();
		}
		
		public abstract void OnExitState();
		public abstract void OnEnterState();
	}
}

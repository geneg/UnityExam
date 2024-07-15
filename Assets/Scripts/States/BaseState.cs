
using Common;
using Common.Services;
using Features.Lobby.Events;

namespace States
{
	public abstract class BaseState : IState
	{
		protected readonly StateMachine StateMachine;
		protected readonly LoaderService LoaderService;
		protected readonly ConfigService ConfigService;
		protected readonly DataService DataService;
		protected readonly AppConfig AppConfig;
		
		protected BaseState(StateMachine stateMachine, ServiceResolver serviceResolver)
		{
			StateMachine = stateMachine;
			LoaderService = serviceResolver.Get<LoaderService>();
			ConfigService = serviceResolver.Get<ConfigService>();
			DataService = serviceResolver.Get<DataService>();
			AppConfig = ConfigService.GetConfig<AppConfig>();
		}

		public virtual void OnExitState()
		{
			EventBroadcaster.Remove<ViewLoadedEvent>(OnLoadedHandler);
		}
		
		public virtual void OnEnterState()
		{
			EventBroadcaster.Add<ViewLoadedEvent>(OnLoadedHandler);
		}
		
		private void OnLoadedHandler(ViewLoadedEvent e)
		{
			OnStateReady(e.View);
		}

		protected abstract void OnStateReady(BaseView view);
	}
}

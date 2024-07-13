
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
		protected readonly AppConfig AppConfig;
		
		protected BaseState(StateMachine stateMachine, ServiceResolver serviceResolver)
		{
			StateMachine = stateMachine;
			LoaderService = serviceResolver.Get<LoaderService>();
			ConfigService = serviceResolver.Get<ConfigService>();
			AppConfig = ConfigService.GetConfig<AppConfig>();
		}

		public virtual void OnExitState()
		{
			EventBroadcaster.Remove<ViewLoadedEvent>(OnViewLoadedHandler);
		}
		
		public virtual void OnEnterState()
		{
			EventBroadcaster.Add<ViewLoadedEvent>(OnViewLoadedHandler);
		}
		
		private void OnViewLoadedHandler(ViewLoadedEvent e)
		{
			OnViewSet(e.View);
		}

		protected abstract void OnViewSet(BaseView view);
	}
}

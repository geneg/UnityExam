using Common;
using Common.Services;
using DefaultNamespace;
using Features.Lobby;
using Features.Lobby.Events;
using UnityEngine;

namespace States
{
	public class LobbyState : BaseState
	{
		private LobbyView _lobbyView; 
		public LobbyState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver) { }
		
		public override void OnEnterState()
		{
			base.OnEnterState();
			LoaderService.LoadScene(AppConfig.GetSceneName(SceneKey.LobbyScene));
		}
		protected override void OnViewSet(BaseView view)
		{
			_lobbyView = view as LobbyView;
			_lobbyView.OnPlay += OnPlayHandler;
		}
		
		private void OnPlayHandler()
		{
			StateMachine.ChangeState<GameState>();
		}
		
		public override void OnExitState()
		{
			base.OnExitState();
			_lobbyView.OnPlay -= OnPlayHandler;
		}
		
	}
}

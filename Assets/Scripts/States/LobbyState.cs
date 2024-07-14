using Common;
using Features.Lobby;

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
		protected override void OnStateReady(BaseView view)
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

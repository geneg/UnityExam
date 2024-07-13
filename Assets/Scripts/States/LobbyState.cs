using Common;
using Common.Services;
using DefaultNamespace;
using Features.Lobby;

namespace States
{
	public class LobbyState : BaseState
	{
		private LobbyView _lobbyView; 
		public LobbyState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver) { }
		
		public override void OnEnterState()
		{
			LoaderService.OnLoadComplete += OnLoadCompleteHandler;
			LoaderService.LoadScene(AppConfig.GetSceneName(SceneKey.LobbyScene));
		}
		
		private void OnLoadCompleteHandler()
		{
			LoaderService.OnLoadComplete -= OnLoadCompleteHandler;
			
			if (!StateMachine.UnityHelper.TryFindObjectOfType(out _lobbyView))
			{
				throw new System.Exception("LobbyView not found.");
			}
			
			_lobbyView.OnPlay += OnPlayHandler;
		}

		private void OnPlayHandler()
		{
			StateMachine.ChangeState<GameState>();
		}
		
		public override void OnExitState()
		{
			_lobbyView.OnPlay -= OnPlayHandler;
		}
		
	}
}

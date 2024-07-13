using Common;
using Common.Services;
using DefaultNamespace;
using Features.Result;

namespace States
{
	public class GameResultState : BaseState
	{
		private ResultView _resultView;

		public GameResultState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver) { }
		
		public override void OnEnterState()
		{
			LoaderService.OnLoadComplete += OnLoadCompleteHandler;
			LoaderService.LoadScene(AppConfig.GetSceneName(SceneKey.ResultsScene));
		}
		
		private void OnPlayHandler()
		{
			StateMachine.ChangeState<GameState>();
		}
		
		private void OnReturnHandler()
		{
			StateMachine.ChangeState<LobbyState>();
		}
		
		public override void OnExitState()
		{
			
			_resultView.OnPlay -= OnPlayHandler;
			_resultView.OnReturn -= OnReturnHandler;
		}
		
		private void OnLoadCompleteHandler()
		{
			LoaderService.OnLoadComplete -= OnLoadCompleteHandler;
			
			if (!StateMachine.UnityHelper.TryFindObjectOfType(out _resultView))
			{
				throw new System.Exception("ResultView not found.");
			}
			
			_resultView.OnPlay += OnPlayHandler;
			_resultView.OnReturn += OnReturnHandler;
		}
	}
}

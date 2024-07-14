using Common;
using Features.Result;

namespace States
{
	public class ResultState : BaseState
	{
		private ResultView _resultView;

		public ResultState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver) { }
		
		public override void OnEnterState()
		{
			base.OnEnterState();
			LoaderService.LoadScene(AppConfig.GetSceneName(SceneKey.ResultsScene));
		}
		
		protected override void OnStateReady(BaseView view)
		{
			_resultView = view as ResultView;
			
			_resultView.OnPlay += OnPlayHandler;
			_resultView.OnReturn += OnReturnHandler;
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
			base.OnExitState();
			
			_resultView.OnPlay -= OnPlayHandler;
			_resultView.OnReturn -= OnReturnHandler;
		}
	}
}

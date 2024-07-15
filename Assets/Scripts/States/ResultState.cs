using Common;
using Data;
using Features.Result;
using UnityEngine;

namespace States
{
	public class ResultState : BaseState
	{
		private ResultView _resultView;

		public ResultState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver) { }
		
		public override void OnEnterState()
		{
			base.OnEnterState();
			
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = false;
			
			PlayerDataModel playerData = DataService.Get<PlayerDataModel>();
			LevelsConfig levelsConfig = ConfigService.GetConfig<LevelsConfig>();
			
			if (playerData.CurrentLevel < levelsConfig.GetLevelsCount() - 1)
				playerData.SetNextLevel();
			
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

using System;
using Common;
using Features.Game;
using Features.Game.FlightPart;
using Features.Game.GroundPart;

namespace States
{
	public class GameState : BaseState
	{
		private readonly GamePartsPlayer _gamePartsPlayer;

		public GameState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver)
		{
			_gamePartsPlayer = new GamePartsPlayer();
		}
		
		public override void OnEnterState()
		{
			base.OnEnterState();
			//get next level data
			
			_gamePartsPlayer.OnSequenceEnd += OnSequenceEndHandler;
			
			LoaderService.LoadScene(AppConfig.GetSceneName(SceneKey.GameScene));
		}
		
		protected override void OnStateReady(BaseView view)
		{
			GameView gameView = view as GameView;

			if (gameView == null)
			{
				throw new InvalidOperationException("No Game State View Found");
			}
			
			FlightPartView flightPartView= gameView.GetPart<FlightPartView>();
			GroundPartView groundPartView= gameView.GetPart<GroundPartView>();
			
			var sharedData = new SharedDataModel();
			_gamePartsPlayer.AddGamePart(new FlightController(flightPartView, sharedData));
			_gamePartsPlayer.AddGamePart(new GroundController(groundPartView, sharedData));
			
			_gamePartsPlayer.StartPlay();
		}

		private void OnSequenceEndHandler()
		{
			StateMachine.ChangeState<ResultState>();
		}
		
		public override void OnExitState()
		{
			base.OnExitState();
			_gamePartsPlayer.OnSequenceEnd -= OnSequenceEndHandler;
			_gamePartsPlayer.Clear();
		}

	}
}

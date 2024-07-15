using System;
using Common;

using Data;
using Features.Game;
using Features.Game.FlightPart;
using Features.Game.GroundPart;
using Features.Items;

namespace States
{
	public class GameState : BaseState
	{
		private readonly GamePartsPlayer _gamePartsPlayer;
		private readonly CollectableItemsController _collectableItemsController;

		public GameState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver)
		{
			PlayerDataModel playerData = DataService.Get<PlayerDataModel>();
			LevelConfig levelConfig = ConfigService.GetConfig<LevelsConfig>().GetLevelConfig(playerData.CurrentLevel);
			
			_gamePartsPlayer = new GamePartsPlayer();
			_collectableItemsController = new CollectableItemsController(ConfigService.GetConfig<CollectableItemsConfig>(), levelConfig);
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

using System;
using Common;
using Common.Services;
using Data;
using Features.Game;
using Features.Game.FlightPart;
using Features.Game.GroundPart;
using Features.Items;

namespace States
{
	public class GameState : BaseState
	{
		private  GamePartsPlayer _gamePartsPlayer;
		private  CollectableItemsController _collectableItemsController;
		private LevelConfig _levelConfig;
		public GameState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver)
		{
		}

		public override void OnEnterState()
		{
			base.OnEnterState();
			
			PlayerDataModel playerData = DataService.Get<PlayerDataModel>();
			_levelConfig = ConfigService.GetConfig<LevelsConfig>().GetLevelConfig(playerData.CurrentLevel);
			
			_gamePartsPlayer = new GamePartsPlayer();
			_collectableItemsController = new CollectableItemsController(ConfigService.GetConfig<CollectableItemsConfig>(), _levelConfig, playerData);

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
			_gamePartsPlayer.AddGamePart(new GroundController(groundPartView, sharedData, _levelConfig));
			
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

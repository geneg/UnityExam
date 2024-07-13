using Common;
using Common.Services;
using DefaultNamespace;
using Features.Game;

namespace States
{
	public class GameState : BaseState
	{
		
		private readonly GamePartsPlayer _gamePartsPlayer;

		public GameState(StateMachine stateMachine, ServiceResolver serviceResolver) : base(stateMachine, serviceResolver)
		{
			_gamePartsPlayer = new GamePartsPlayer();
		}
		
		public override void OnExitState()
		{
			_gamePartsPlayer.OnSequenceEnd -= OnSequenceEndHandler;
			_gamePartsPlayer.Reset();
		}
		
		public override void OnEnterState()
		{
			_gamePartsPlayer.AddGamePart(new FlightController(StateMachine.UnityHelper));
			_gamePartsPlayer.AddGamePart(new GroundController(StateMachine.UnityHelper));
			
			_gamePartsPlayer.OnSequenceEnd += OnSequenceEndHandler;
			
			LoaderService.OnLoadComplete += OnLoadCompleteHandler;
			LoaderService.LoadScene(AppConfig.GetSceneName(SceneKey.GameScene));
		}
		
		private void OnLoadCompleteHandler()
		{
			LoaderService.OnLoadComplete -= OnLoadCompleteHandler;
			_gamePartsPlayer.StartPlay();
		}

		private void OnSequenceEndHandler()
		{
			StateMachine.ChangeState<GameResultState>();
		}
	}
}

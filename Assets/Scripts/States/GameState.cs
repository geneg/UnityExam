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
		
		public override void OnEnterState()
		{
			base.OnEnterState();
			//get next level data
			
			_gamePartsPlayer.AddGamePart(new FlightController(StateMachine.UnityHelper));
			_gamePartsPlayer.AddGamePart(new GroundController(StateMachine.UnityHelper));
			
			_gamePartsPlayer.OnSequenceEnd += OnSequenceEndHandler;
			
			LoaderService.LoadScene(AppConfig.GetSceneName(SceneKey.GameScene));
		}
		
		protected override void OnViewSet(BaseView view)
		{
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

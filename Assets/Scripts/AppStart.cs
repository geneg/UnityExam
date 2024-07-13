using Common;
using Common.Services;
using States;

public class AppStart : EntryPoint
{
    private ServiceResolver _serviceResolver;
    private StateMachine _stateMachine;
    
    protected override void OnAwake()
    {
        _serviceResolver = new ServiceResolver();
        
        _serviceResolver.Register(new ConfigService());
        _serviceResolver.Register(new LoaderService(UnityHelper));
        
        _stateMachine = new StateMachine(_serviceResolver, UnityHelper);
        
        _stateMachine.AddState<LobbyState>();
        _stateMachine.AddState<GameState>();
        _stateMachine.AddState<GameResultState>();
    }
    
    protected override void OnStart()
    {
        _stateMachine.SetDefaultState<LobbyState>();
    }
    
    protected override void OnAppDestroy()
    {
    }
}
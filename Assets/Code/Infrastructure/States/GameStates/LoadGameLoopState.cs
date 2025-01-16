using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
    public class LoadGameLoopState : IState
    {
        private const string GameLoopSceneName = "GameLoopScene";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadGameLoopState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
    
        public void Enter()
        {
            _sceneLoader.LoadScene(GameLoopSceneName, EnterGameLoopState);
        }

        private void EnterGameLoopState()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}
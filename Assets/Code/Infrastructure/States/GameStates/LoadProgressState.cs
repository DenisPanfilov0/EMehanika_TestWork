using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;

namespace Code.Infrastructure.States.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progress;

        public LoadProgressState(IGameStateMachine stateMachine, IProgressProvider progress)
        {
            _stateMachine = stateMachine;
            _progress = progress;
        }
    
        public void Enter()
        {
            CreateNewProgress();
            
            EnterMainMenuState();
        }

        private void EnterMainMenuState()
        {
            _stateMachine.Enter<LoadMainMenuState>();
        }

        public void Exit()
        {
      
        }
        
        private void CreateNewProgress()
        {
            _progress.SetProgressData(new ProgressData());
        }
    }
}
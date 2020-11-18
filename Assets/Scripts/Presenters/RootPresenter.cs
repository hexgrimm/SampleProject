using Models;

namespace Presenters
{	//General orchestrator of an application execution flow. Fully non-Unity
	public class RootPresenter
	{
		private readonly IRootModel _rootModel;
		
		private PresenterStateBase _currentState;
		
		public RootPresenter(IRootModel rootModel, IPresenterStateFactory stateFactory)
		{
			_rootModel = rootModel;
			SetNewState(stateFactory.CreateLoadingState());
		}
		
		public void Update()
		{
			_rootModel.Update();
		}

		private void SetNewState(PresenterStateBase newState)
		{
			_currentState?.OnExit();
			newState.Context = this;
			_currentState = newState;
			_currentState.OnEnter();
		}
	}
}
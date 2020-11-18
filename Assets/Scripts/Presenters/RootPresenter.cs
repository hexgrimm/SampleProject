using Models;
using Views;

namespace Presenters
{	//General orchestrator of an application execution flow. Fully non-Unity
	public class RootPresenter
	{
		private readonly IRootModel _rootModel;
		private readonly IPresenterStateFactory _stateFactory;
		private readonly IUpdater _updater;

		private PresenterStateBase _currentState;
		
		public RootPresenter(IRootModel rootModel, IPresenterStateFactory stateFactory, IUpdater updater)
		{
			_rootModel = rootModel;
			_stateFactory = stateFactory;
			updater.UpdateEvent += Update;
			SetNewState(stateFactory.CreateLoadingState());
		}

		private void Update()
		{
			_currentState?.PreModelUpdate();
			_rootModel.Update();
			_currentState?.PostModelUpdate();
		}

		private void SetNewState(PresenterStateBase newState)
		{
			_currentState?.OnExit();
			newState.SetNewState = SetNewState;
			newState.Factory = _stateFactory;
			_currentState = newState;
			_currentState.OnEnter();
		}
	}
}
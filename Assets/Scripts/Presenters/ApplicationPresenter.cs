using Models;
using Views;

namespace Presenters
{	//General orchestrator of an application execution flow. Fully non-Unity
	public class ApplicationPresenter
	{
		protected readonly IApplicationModel AppModel;
		protected readonly IApplicationView AppView;

		private PresenterStateBase _currentState;
		
		public ApplicationPresenter(IApplicationModel appModel, IApplicationView appView)
		{
			AppModel = appModel;
			AppView = appView;
		}
		
		public void Update()
		{
			AppModel.Update();
		}

		private void SetNewState(PresenterStateBase newState)
		{
			_currentState?.OnExit();
			_currentState = newState;
			_currentState.OnEnter();
		}
	}
}
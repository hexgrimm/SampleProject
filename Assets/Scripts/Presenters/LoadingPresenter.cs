using EventUtils;
using Models;
using Views;

namespace Presenters
{
	public class LoadingPresenter : PresenterStateBase
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IAppInitModel _appInitModel;
		private readonly IPresenterStateFactory _stateFactory;

		private EventDelay _loadCompleteEvent = new EventDelay(); 
		
		public LoadingPresenter(ILoadingWindowView loadingWindowView, IAppInitModel appInitModel)
		{
			_loadingWindowView = loadingWindowView;
			_appInitModel = appInitModel;
		}
		
		public override void OnEnter()
		{
			_loadingWindowView.Show();
			_loadingWindowView.EnableSpinnerRotation();
			_appInitModel.AllPluginsInitialized += _loadCompleteEvent.DirectMethod;
			_loadCompleteEvent.SetCallback(OnModelLoadComplete);
		}
		
		public override void OnExit()
		{
			_loadingWindowView.Hide();
			_appInitModel.AllPluginsInitialized -= _loadCompleteEvent.DirectMethod;
		}

		private void OnModelLoadComplete()
		{
			SetNewState(_stateFactory.CreateLobbyState());
		}
	}
}
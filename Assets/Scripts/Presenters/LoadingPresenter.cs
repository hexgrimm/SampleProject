using EventUtils;
using Models;
using Views;

namespace Presenters
{
	public class LoadingPresenter : PresenterStateBase
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IAppInitModel _appInitModel;

		private readonly EventDelay _loadCompleteEvent = new EventDelay(); 
		
		public LoadingPresenter(ILoadingWindowView loadingWindowView, IAppInitModel appInitModel)
		{
			_loadingWindowView = loadingWindowView;
			_appInitModel = appInitModel;
		}
		
		public override void OnEnter()
		{
			_loadingWindowView.Show();
			_loadingWindowView.EnableSpinnerRotation();
			_appInitModel.AllPluginsInitialized += _loadCompleteEvent.DirectCallMethod;
			_loadCompleteEvent.SetCallback(OnModelLoadComplete);
		}
		
		public override void OnExit()
		{
			_loadingWindowView.Hide();
			_appInitModel.AllPluginsInitialized -= _loadCompleteEvent.DirectCallMethod;
		}

		public override void PostModelUpdate()
		{
			base.PostModelUpdate();
			_loadCompleteEvent.ProceedReactionIfNeeded(); //this way we can reorder reactions of every model change or skip some which are unnecessary (_loadCompleteEvent.ClearCalls)
		}

		private void OnModelLoadComplete()
		{
			SetNewState(StateFactory.CreateLobbyState());
		}
	}
}
using Views;

namespace Presenters
{
	public class LoadingPresenter : PresenterStateBase
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IAppInitModel _appInitModel;

		public LoadingPresenter(ILoadingWindowView loadingWindowView, IAppInitModel appInitModel)
		{
			_loadingWindowView = loadingWindowView;
			_appInitModel = appInitModel;
		}
		
		public override void OnEnter()
		{
			_loadingWindowView.Show();
			_loadingWindowView.RotateSpinner();
		}

		public override void OnExit()
		{
			_loadingWindowView.Hide();
		}
	}
}
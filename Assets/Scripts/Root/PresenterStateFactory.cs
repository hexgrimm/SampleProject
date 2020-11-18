using Presenters;
using Views;

namespace Root
{
	public class PresenterStateFactory : IPresenterStateFactory
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IAppInitModel _appInitModel;

		public PresenterStateFactory(ILoadingWindowView loadingWindowView, IAppInitModel appInitModel)
		{
			_loadingWindowView = loadingWindowView;
			_appInitModel = appInitModel;
		}
		
		public PresenterStateBase CreateLoadingState()
		{
			return new LoadingPresenter(_loadingWindowView, _appInitModel);
		}
	}
}
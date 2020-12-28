using Models;
using Models.App;
using Models.Assets;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class LoadingWindowPresenter : IUpdateablePresenter
	{
		private readonly ILoadingWindow _loadingWindow;
		private readonly IApplicationModel _applicationModel;
		private readonly WindowSubPresenter _windowSubPresenter;

		public LoadingWindowPresenter(ILoadingWindow loadingWindow, IApplicationModel applicationModel)
		{
			_loadingWindow = loadingWindow;
			_applicationModel = applicationModel;
			_windowSubPresenter = new WindowSubPresenter(applicationModel, ResourceId.LoadingWindow, _loadingWindow);
		}
		
		public void PreModelUpdate()
		{
			
		}

		public void PostModelUpdate()
		{
			_windowSubPresenter.SyncWindowState();
		}

	}
}
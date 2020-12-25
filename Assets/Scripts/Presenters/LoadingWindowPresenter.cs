using Models;
using Models.App;
using Models.Assets;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class LoadingWindowPresenter : IUpdateablePresenter
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IApplicationModel _applicationModel;

		public LoadingWindowPresenter(ILoadingWindowView loadingWindowView, IApplicationModel applicationModel)
		{
			_loadingWindowView = loadingWindowView;
			_applicationModel = applicationModel;
		}
		
		public void PreModelUpdate()
		{
			
		}

		public void PostModelUpdate()
		{
			if (_applicationModel.LayersChanged.Get)
			{
				UpdateViewState();
			}
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _applicationModel.Layers.Count; i++)
			{
				var item = _applicationModel.Layers[i];
				if (item.viewId == ResourceId.LoadingWindow)
				{
					_loadingWindowView.ShowOnLayer(i, item.prefab);
					return;
				}
			}

			_loadingWindowView.Hide();
		}
	}
}
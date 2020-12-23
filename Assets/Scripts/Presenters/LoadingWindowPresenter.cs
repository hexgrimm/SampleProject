using Models;
using Models.App;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class LoadingWindowPresenter : IUpdateablePresenter
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IApplicationModel _applicationModel;
		private bool _dataTransferEnabled;

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
			
			if (!_dataTransferEnabled)
				return;
			
			
			_loadingWindowView.EnableSpinnerRotation();
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _applicationModel.Layers.Count; i++)
			{
				var item = _applicationModel.Layers[i];
				if (item == (int) ResourcesConfiguration.ResourceId.LoadingWindow)
				{
					_loadingWindowView.ShowOnLayer(i);
					_dataTransferEnabled = true;
					return;
				}
			}

			_loadingWindowView.Hide();
			_dataTransferEnabled = false;
		}
	}
}
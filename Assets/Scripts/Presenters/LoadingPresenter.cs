using Models;
using Models.ApplicationViewModel;
using Views;

namespace Presenters
{
	public class LoadingPresenter : IUpdateablePresenter
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IApplicationViewModel _applicationViewModel;
		private bool _dataTransferEnabled;

		public LoadingPresenter(ILoadingWindowView loadingWindowView, IApplicationViewModel applicationViewModel)
		{
			_loadingWindowView = loadingWindowView;
			_applicationViewModel = applicationViewModel;
		}
		
		public void PreModelUpdate()
		{
			
		}

		public void PostModelUpdate()
		{
			if (_applicationViewModel.LayersChanged.Get)
			{
				UpdateViewState();
			}
			
			if (!_dataTransferEnabled)
				return;
			
			
			_loadingWindowView.EnableSpinnerRotation();
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _applicationViewModel.Layers.Count; i++)
			{
				var item = _applicationViewModel.Layers[i];
				if (item == ViewsConfiguration.LobbyViewId)
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
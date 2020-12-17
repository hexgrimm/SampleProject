using Models;
using Models.ApplicationViewModel;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class LoadingWindowPresenter : IUpdateablePresenter
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IRootModel _rootModel;
		private bool _dataTransferEnabled;

		public LoadingWindowPresenter(ILoadingWindowView loadingWindowView, IRootModel rootModel)
		{
			_loadingWindowView = loadingWindowView;
			_rootModel = rootModel;
		}
		
		public void PreModelUpdate()
		{
			
		}

		public void PostModelUpdate()
		{
			if (_rootModel.LayersChanged.Get)
			{
				UpdateViewState();
			}
			
			if (!_dataTransferEnabled)
				return;
			
			
			_loadingWindowView.EnableSpinnerRotation();
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _rootModel.Layers.Count; i++)
			{
				var item = _rootModel.Layers[i];
				if (item == (int) ViewsConfiguration.ViewWindowId.LoadingViewWindowId)
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
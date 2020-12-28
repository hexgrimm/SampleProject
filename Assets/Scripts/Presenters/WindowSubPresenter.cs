using Models.App;
using Models.Assets;
using Views;

namespace Presenters
{
	public class WindowSubPresenter
	{
		private readonly IApplicationModel _applicationModel;
		private readonly ResourceId _windowResourceId;
		private readonly IWindow _window;

		public WindowSubPresenter(IApplicationModel applicationModel, ResourceId windowResourceId, IWindow window)
		{
			_applicationModel = applicationModel;
			_windowResourceId = windowResourceId;
			_window = window;
		}
		
		public bool WindowHasShown { get; private set; }
		
		public void SyncWindowState()
		{
			if (!_applicationModel.LayersChanged.Get)
				return;
			
			for (int i = 0; i < _applicationModel.Layers.Count; i++)
			{
				var item = _applicationModel.Layers[i];
				if (item.viewId == _windowResourceId)
				{
					_window.ShowOnLayer(i, item.prefab);
					WindowHasShown = true;
					return;
				}
			}

			WindowHasShown = false;
			_window.Hide();
		}
	}
}
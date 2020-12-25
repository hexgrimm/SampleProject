using Models;
using Models.App;
using Models.Assets;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class GameWindowPresenter : IUpdateablePresenter
	{
		private readonly IApplicationModel _applicationModel;
		private readonly IGameWindow _gameWindow;

		public GameWindowPresenter(IGameWindow gameWindow, IApplicationModel applicationModel)
		{
			_applicationModel = applicationModel;
			_gameWindow = gameWindow;
		}
		
		public void PreModelUpdate()
		{
			if (_gameWindow.QuitGameButton.Get)
				_applicationModel.QuitGame();
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
				if (item == (int) ResourceId.GameWindow)
				{
					//_gameWindow.ShowOnLayer(i);
					return;
				}
			}

			_gameWindow.Hide();
		}
	}
}
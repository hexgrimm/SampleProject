using Models;
using Models.ApplicationViewModel;
using Views;

namespace Presenters
{
	public class GameWindowPresenter : IUpdateablePresenter
	{
		private readonly IRootModel _rootModel;
		private readonly IGameWindow _gameWindow;

		public GameWindowPresenter(IGameWindow gameWindow, IRootModel rootModel)
		{
			_rootModel = rootModel;
			_gameWindow = gameWindow;
		}
		
		public void PreModelUpdate()
		{
			if (_gameWindow.QuitGameButton.Get)
				_rootModel.QuitGame();
		}

		public void PostModelUpdate()
		{
			if (_rootModel.LayersChanged.Get)
			{
				UpdateViewState();
			}
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _rootModel.Layers.Count; i++)
			{
				var item = _rootModel.Layers[i];
				if (item == (int) ViewsConfiguration.ViewWindowId.GameViewWindowId)
				{
					_gameWindow.ShowOnLayer(i);
					return;
				}
			}

			_gameWindow.Hide();
		}
	}
}
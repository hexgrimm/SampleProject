using Models;
using Models.App;
using Models.Assets;
using Models.Layers;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class GameWindowPresenter : IUpdateablePresenter
	{
		private readonly IApplicationModel _applicationModel;
		private readonly IGameWindow _gameWindow;
		private readonly WindowSubPresenter _windowSubPresenter;

		public GameWindowPresenter(IGameWindow gameWindow, IApplicationModel applicationModel)
		{
			_applicationModel = applicationModel;
			_gameWindow = gameWindow;
			_windowSubPresenter = new WindowSubPresenter(applicationModel, ResourceId.GameWindow, _gameWindow);
		}
		
		public void PreModelUpdate()
		{
			if (_gameWindow.QuitGameButton.Get)
				_applicationModel.QuitGame();
		}

		public void PostModelUpdate()
		{
			_windowSubPresenter.SyncWindowState();
		}
	}
}
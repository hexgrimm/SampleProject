using Models.App;
using Models.Assets;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class LobbyWindowPresenter : IUpdateablePresenter
	{
		private readonly ILobbyWindow _lobbyWindow;
		private readonly IApplicationModel _applicationModel;
		private readonly WindowSubPresenter _windowSubPresenter;

		public LobbyWindowPresenter(ILobbyWindow lobbyWindow, IApplicationModel applicationModel)
		{
			_lobbyWindow = lobbyWindow;
			_applicationModel = applicationModel;
			_windowSubPresenter = new WindowSubPresenter(applicationModel, ResourceId.LobbyWindow, _lobbyWindow);
		}

		public void PreModelUpdate()
		{
			if (_lobbyWindow.RequestCoinsButton.Get)
			{
				_applicationModel.RequestMoreCoins();
			}
			else if (_lobbyWindow.StartGameButton.Get)
			{
				_applicationModel.StartNewGame();
			}
		}

		public void PostModelUpdate()
		{
			_windowSubPresenter.SyncWindowState();
			
			if (!_windowSubPresenter.WindowHasShown)
				return;
			
			_lobbyWindow.SetDeltaTimeValue(_applicationModel.DeltaTime);
			_lobbyWindow.SetCoinsValue(_applicationModel.Coins);
		}
		
	}
}
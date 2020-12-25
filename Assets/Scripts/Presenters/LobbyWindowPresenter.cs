using Models;
using Models.App;
using Models.Assets;
using Presenters.CoreLoop;
using Views;

namespace Presenters
{
	public class LobbyWindowPresenter : IUpdateablePresenter
	{
		private readonly ILobbyView _lobbyView;
		private readonly IApplicationModel _applicationModel;

		private bool _dataTransferEnabled;

		public LobbyWindowPresenter(ILobbyView lobbyView, IApplicationModel applicationModel)
		{
			_lobbyView = lobbyView;
			_applicationModel = applicationModel;
		}

		public void PreModelUpdate()
		{
			if (_lobbyView.RequestCoinsButton.Get)
			{
				_applicationModel.RequestMoreCoins();
			}
			else if (_lobbyView.StartGameButton.Get)
			{
				_applicationModel.StartNewGame();
			}
		}

		public void PostModelUpdate()
		{
			if (_applicationModel.LayersChanged.Get)
			{
				UpdateViewState();
			}
			
			if (!_dataTransferEnabled)
				return;
			
			_lobbyView.SetDeltaTimeValue(_applicationModel.DeltaTime);
			_lobbyView.SetCoinsValue(_applicationModel.Coins);
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _applicationModel.Layers.Count; i++)
			{
				var item = _applicationModel.Layers[i];
				if (item.viewId == ResourceId.LobbyWindow)
				{
					_lobbyView.ShowOnLayer(i, item.prefab);
					_dataTransferEnabled = true;
					return;
				}
			}

			_lobbyView.Hide();
			_dataTransferEnabled = false;
		}
	}
}
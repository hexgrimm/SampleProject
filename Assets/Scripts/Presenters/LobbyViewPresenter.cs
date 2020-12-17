using Models;
using Models.ApplicationViewModel;
using Views;

namespace Presenters
{
	public class LobbyViewPresenter : IUpdateablePresenter
	{
		private readonly ILobbyView _lobbyView;
		private readonly IRootModel _rootModel;

		private bool _dataTransferEnabled;

		public LobbyViewPresenter(ILobbyView lobbyView, IRootModel rootModel)
		{
			_lobbyView = lobbyView;
			_rootModel = rootModel;
		}

		public void PreModelUpdate()
		{
			if (_lobbyView.RequestCoinsButton.Get)
			{
				_rootModel.RequestMoreCoins.Raise();
			}
			else if (_lobbyView.StartGameButton.Get)
			{
				_rootModel.StartNewGame();
			}
		}

		public void PostModelUpdate()
		{
			if (_rootModel.LayersChanged.Get)
			{
				UpdateViewState();
			}
			
			if (!_dataTransferEnabled)
				return;
			
			_lobbyView.SetDeltaTimeValue(_rootModel.DeltaTime);
			_lobbyView.SetCoinsValue(_rootModel.Coins);
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _rootModel.Layers.Count; i++)
			{
				var item = _rootModel.Layers[i];
				if (item == (int) ViewsConfiguration.ViewWindowId.LobbyViewWindowId)
				{
					_lobbyView.ShowOnLayer(i);
					_dataTransferEnabled = true;
					return;
				}
			}

			_lobbyView.Hide();
			_dataTransferEnabled = false;
		}
	}
}
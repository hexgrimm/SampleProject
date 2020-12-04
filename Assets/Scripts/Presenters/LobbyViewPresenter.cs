using Models;
using Models.ApplicationViewModel;
using Views;

namespace Presenters
{
	public class LobbyViewPresenter : IUpdateablePresenter
	{
		private readonly ILobbyView _lobbyView;
		private readonly IApplicationViewModel _applicationViewModel;

		private bool _dataTransferEnabled;

		public LobbyViewPresenter(ILobbyView lobbyView, IApplicationViewModel applicationViewModel)
		{
			_lobbyView = lobbyView;
			_applicationViewModel = applicationViewModel;
		}

		public void PreModelUpdate()
		{
			if (_lobbyView.RequestCoinsButton.Get)
			{
				_applicationViewModel.RequestMoreCoins.Raise();
			}
		}

		public void PostModelUpdate()
		{
			if (_applicationViewModel.LayersChanged.Get)
			{
				UpdateViewState();
			}
			
			if (!_dataTransferEnabled)
				return;
			
			_lobbyView.SetDeltaTimeValue(_applicationViewModel.DeltaTime);
			_lobbyView.SetCoinsValue(_applicationViewModel.Coins);
		}

		private void UpdateViewState()
		{
			for (int i = 0; i < _applicationViewModel.Layers.Count; i++)
			{
				var item = _applicationViewModel.Layers[i];
				if (item == ViewsConfiguration.LobbyViewId)
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
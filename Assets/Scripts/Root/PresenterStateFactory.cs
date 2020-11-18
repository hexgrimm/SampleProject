using Models;
using Presenters;
using Views;

namespace Root
{
	public class PresenterStateFactory : IPresenterStateFactory
	{
		private readonly ILoadingWindowView _loadingWindowView;
		private readonly IAppInitModel _appInitModel;
		private readonly ILobbyView _lobbyView;
		private readonly IPlayerBalanceModel _playerBalanceModel;
		private readonly ITimeModel _timeModel;

		public PresenterStateFactory(ILoadingWindowView loadingWindowView, IAppInitModel appInitModel, 
			ILobbyView lobbyView, IPlayerBalanceModel playerBalanceModel, ITimeModel timeModel)
		{
			_loadingWindowView = loadingWindowView;
			_appInitModel = appInitModel;
			_lobbyView = lobbyView;
			_playerBalanceModel = playerBalanceModel;
			_timeModel = timeModel;
		}
		
		public PresenterStateBase CreateLoadingState()
		{
			return new LoadingPresenter(_loadingWindowView, _appInitModel);
		}

		public PresenterStateBase CreateLobbyState()
		{
			return new LobbyPresenter(_lobbyView, _playerBalanceModel, _timeModel);
		}
	}
}
using Models;
using Views;

namespace Presenters
{
	public class LobbyPresenter : PresenterStateBase
	{
		private readonly ILobbyView _lobbyView;
		private readonly IPlayerBalanceModel _balanceModel;

		public LobbyPresenter(ILobbyView lobbyView, IPlayerBalanceModel balanceModel)
		{
			_lobbyView = lobbyView;
			_balanceModel = balanceModel;
		}
		
		public override void OnEnter()
		{
			
		}

		private void OnRequestButtonPressed()
		{
			
		}

		public override void OnExit()
		{
			
		}
	}
}
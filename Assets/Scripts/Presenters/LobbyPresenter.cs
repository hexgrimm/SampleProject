using EventUtils;
using Models;
using Views;

namespace Presenters
{
	public class LobbyPresenter : PresenterStateBase
	{
		private readonly ILobbyView _lobbyView;
		private readonly IPlayerBalanceModel _balanceModel;
		private readonly ITimeModel _timeModel;

		private readonly EventDelay _coinsChangedEvent = new EventDelay(); 
		
		public LobbyPresenter(ILobbyView lobbyView, IPlayerBalanceModel balanceModel, ITimeModel timeModel)
		{
			_lobbyView = lobbyView;
			_balanceModel = balanceModel;
			_timeModel = timeModel;
		}
		
		public override void OnEnter()
		{
			_lobbyView.Show();
			_lobbyView.SetCoinsValue(_balanceModel.Coins);
			_lobbyView.SubscribeToRequestButton(OnRequestButtonPressed);
			
			_balanceModel.CoinsValueChanged += _coinsChangedEvent.DirectCallMethod;
			_coinsChangedEvent.SetCallback(OnCoinsValueChanged);
		}
		
		public override void PostModelUpdate() //example of polling values. Useful for dynamic values or positions, where skipping frames is fatal
		{
			base.PostModelUpdate();
			_lobbyView.SetDeltaTimeValue(_timeModel.DeltaTime);
			
			_coinsChangedEvent.ProceedReactionIfNeeded();
		}

		public override void OnExit()
		{
			_balanceModel.CoinsValueChanged -= _coinsChangedEvent.DirectCallMethod;
		}
		
		private void OnRequestButtonPressed()
		{
			_balanceModel.RequestMoreCoins();
		}
		
		private void OnCoinsValueChanged()
		{
			_lobbyView.SetCoinsValue(_balanceModel.Coins); //better to use generic events with arguments, i skipped it in example. There are all notifyPropertyChanged patterns possible
		}
	}
}
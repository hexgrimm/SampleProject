using EventUtils;
using Models;
using Models.ViewLayersModel;
using Views;

namespace Presenters
{
	public class LobbyPresenter : PresenterStateBase
	{
		private readonly ILobbyView _lobbyView;
		private readonly IMetaModel _balanceModel;
		private readonly ITimeModel _timeModel;

		private readonly EventDelay _coinsChangedDelay = new EventDelay(); 
		
		public LobbyPresenter(ILobbyView lobbyView, IMetaModel balanceModel, ITimeModel timeModel)
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
			
			//_balanceModel.PlayerBalanceChanged += _coinsChangedDelay.MethodForDirectSubscribing;
			_coinsChangedDelay.SetCallback(OnCoinsValueChanged);
		}
		
		public override void PostModelUpdate() //example of polling values. Useful for dynamic values or positions, where skipping frames is fatal
		{
			base.PostModelUpdate();
			_lobbyView.SetDeltaTimeValue(_timeModel.DeltaTime);
			
			_coinsChangedDelay.PollChanges();
		}

		public override void OnExit()
		{
			//_balanceModel.PlayerBalanceChanged -= _coinsChangedDelay.MethodForDirectSubscribing;
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

	public class LobbyWindowSubPresenter
	{
		private readonly IViewLayersModel _viewLayersModel;
		private readonly ILobbyView _lobbyView;
		
		public LobbyWindowSubPresenter(IViewLayersModel viewLayersModel, ILobbyView lobbyView)
		{
			_viewLayersModel = viewLayersModel;
			_lobbyView = lobbyView;
		}

		public void Update()
		{
			
		}
	}
}
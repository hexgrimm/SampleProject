using System.Collections.Generic;
using EventUtils;
using Models.ViewLayersModel;

namespace Models.ApplicationViewModel
{
	//TODO: convert it into a context for state pattern
	public class ApplicationViewModel : IUpdateable, IApplicationViewModel
	{
		private readonly IMetaModel _metaModel;
		private readonly ITimeModel _timeModel;
		private readonly IViewLayersModel _viewLayersModel;
		
		private bool _isInitialized = false;
		private bool _discPopupShown;
		
		private ApplicationViewStates _currentState;
		
		private readonly Signal _requestMoreCoins = new Signal();
		
		private readonly Signal<int> _exchangeCoinsToCrystals = new Signal<int>();

		private readonly Signal _startNewGame = new Signal();
		private readonly Signal<bool> _gameRunning = new Signal<bool>();

		public void StartNewGame()
		{
			_startNewGame.Raise();
		}

		public int Coins => _metaModel.Coins;

		public int Crystals => _metaModel.Crystals;
		public float DeltaTime => _timeModel.DeltaTime;

		public ISignalSource RequestMoreCoins => _requestMoreCoins;

		public ISignalSource<int> ExchangeCoinsToCrystals => _exchangeCoinsToCrystals;

		public ISignal LayersChanged => _viewLayersModel.LayersChanged;

		public ISignal<bool> GameRunning => _gameRunning;

		public IReadOnlyList<int> Layers => _viewLayersModel.Layers;
		

		public ApplicationViewModel(IMetaModel metaModel, ITimeModel timeModel, IViewLayersModel viewLayersModel)
		{
			_metaModel = metaModel;
			_timeModel = timeModel;
			_viewLayersModel = viewLayersModel;
			_currentState = ApplicationViewStates.Loading;
		}
		
		public void Update()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Init();
			}

			if (_currentState == ApplicationViewStates.Loading)
				LoadingUpdate();
			else if (_currentState == ApplicationViewStates.Lobby)
				LobbyUpdate();
			else if (_currentState == ApplicationViewStates.Game)
				GameUpdate();
		}

		private void GameUpdate()
		{
			
		}

		private void LobbyUpdate()
		{
			if (!_metaModel.IsConnected && !_discPopupShown)
			{
				_discPopupShown = true;
				_viewLayersModel.ShowViewOnTop(ViewsConfiguration.DisconnectedPopUpViewId);
			}

			if (_metaModel.IsConnected && _discPopupShown)
			{
				_discPopupShown = false;
				_viewLayersModel.HideView(ViewsConfiguration.DisconnectedPopUpViewId);
			}

			if (_startNewGame.Get)
			{
				TransitFromLobbyToGame();
			}
		}

		private void LoadingUpdate()
		{
			if (_currentState == ApplicationViewStates.Loading)
			{
				if (_metaModel.IsConnected)
				{
					TransitFromLoadingToLobby();
					return;
				}
			}
		}

		private void TransitFromLoadingToLobby()
		{
			_currentState = ApplicationViewStates.Lobby;
			_viewLayersModel.HideAll();
			_viewLayersModel.ShowViewOnTop(ViewsConfiguration.LobbyViewId);
		}
		
		private void TransitFromLobbyToGame()
		{
			_currentState = ApplicationViewStates.Game;
			_viewLayersModel.HideAll();
			_gameRunning.Raise(true);
		}

		private void Init()
		{
			_viewLayersModel.HideAll();
			_viewLayersModel.ShowViewOnTop(ViewsConfiguration.LoadingViewId);
		}


		public enum ApplicationViewStates
		{
			Loading,
			Lobby,
			Game,
		}
	}
}
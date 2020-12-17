using System.Collections.Generic;
using EventUtils;
using Models.Simulation;
using Models.ViewLayersModel;

namespace Models.ApplicationViewModel
{
	//TODO: convert it into a context for a state pattern
	public class ApplicationModel : IUpdateable, IRootModel
	{
		private readonly IMetaModel _metaModel;
		private readonly ITimeModel _timeModel;
		private readonly ILayersModel _layersModel;
		private readonly ISimulationModel _simulationModel;
		private readonly IAssetsModel _assetsModel;
		private readonly IUpdateWatcher _updateWatcher;

		private bool _isInitialized = false;
		private bool _discPopupShown;
		
		private ApplicationViewStates _currentState;
		
		private readonly Signal _requestMoreCoins = new Signal();
		
		private readonly Signal<int> _exchangeCoinsToCrystals = new Signal<int>();

		private readonly Signal _startNewGame = new Signal();
		private readonly Signal _quitGame = new Signal();
		private readonly Signal<bool> _gameRunning = new Signal<bool>();

		public int Coins => _metaModel.Coins;
		public int Crystals => _metaModel.Crystals;
		public float DeltaTime => _timeModel.DeltaTime;

		public ISignalSource RequestMoreCoins => _requestMoreCoins;

		public ISignalSource<int> ExchangeCoinsToCrystals => _exchangeCoinsToCrystals;

		public ISignal LayersChanged => _layersModel.LayersChanged;

		public ISignal<bool> GameRunning => _gameRunning;

		public IReadOnlyList<int> Layers => _layersModel.Layers;
		
		public ApplicationModel(IMetaModel metaModel, ITimeModel timeModel, ILayersModel layersModel,
			ISimulationModel simulationModel, IAssetsModel assetsModel, IUpdateWatcher updateWatcher)
		{
			_metaModel = metaModel;
			_timeModel = timeModel;
			_layersModel = layersModel;
			_simulationModel = simulationModel;
			_assetsModel = assetsModel;
			_updateWatcher = updateWatcher;
			_currentState = ApplicationViewStates.Loading;
		}
		
		public void Update()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Init();
			}
			
			_updateWatcher.RegisterUpdate();
			
			_timeModel.Update();
			_layersModel.Update();
			_assetsModel.Update();
			_simulationModel.Update(_timeModel.DeltaTime);
			
			
			if (_currentState == ApplicationViewStates.Loading)
				LoadingUpdate();
			else if (_currentState == ApplicationViewStates.Lobby)
				LobbyUpdate();
			else if (_currentState == ApplicationViewStates.Game)
				GameUpdate();
		}

		public void StartNewGame()
		{
			_startNewGame.Raise();
		}

		public void QuitGame()
		{
			_quitGame.Raise();
		}

		private void GameUpdate()
		{
			_metaModel.Update();
			
			if (_quitGame.Get)
			{
				TransitFromGameToLobby();
			}
		}

		private void LobbyUpdate()
		{
			_metaModel.Update();
			
			if (!_metaModel.IsConnected && !_discPopupShown)
			{
				_discPopupShown = true;
				_layersModel.ShowViewOnTop((int) ViewsConfiguration.ViewWindowId.DisconnectedPopUpViewId);
			}

			if (_metaModel.IsConnected && _discPopupShown)
			{
				_discPopupShown = false;
				_layersModel.HideView((int) ViewsConfiguration.ViewWindowId.DisconnectedPopUpViewId);
			}

			if (_requestMoreCoins.Get)
			{
				if (_metaModel.IsConnected && !_discPopupShown)
				{
					_metaModel.RequestMoreCoins();
				}
			}

			if (_startNewGame.Get)
			{
				TransitFromLobbyToGame();
			}
		}

		private void LoadingUpdate()
		{
			_metaModel.Update();
			
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
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ViewsConfiguration.ViewWindowId.LobbyViewWindowId);
		}
		
		private void TransitFromLobbyToGame()
		{
			_currentState = ApplicationViewStates.Game;
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ViewsConfiguration.ViewWindowId.GameViewWindowId);
			
			_gameRunning.Raise(true);
			
			_simulationModel.InstantiatePrefab();
			_simulationModel.Show();
		}

		private void TransitFromGameToLobby()
		{
			_currentState = ApplicationViewStates.Lobby;
			
			_simulationModel.Hide();
			_simulationModel.DestroyInstanceForUnload();
			
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ViewsConfiguration.ViewWindowId.LobbyViewWindowId);
		}

		private void Init()
		{
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ViewsConfiguration.ViewWindowId.LoadingViewWindowId);
		}
		
		public enum ApplicationViewStates
		{
			Loading,
			Lobby,
			Game,
		}
	}
}
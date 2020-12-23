using System.Collections.Generic;
using Models.Assets;
using Models.Layers;
using Models.Meta;
using Models.Simulation;
using Models.Times;
using Utils;
using static Models.ResourcesConfiguration;

namespace Models.App
{
	//TODO: convert it into a context for a state pattern
	public class ApplicationModel : IUpdateable, IApplicationModel
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
		
		private readonly Flag _requestMoreCoins = new Flag();
		
		private readonly Flag<int> _exchangeCoinsToCrystals = new Flag<int>();

		private readonly Flag _startNewGame = new Flag();
		private readonly Flag _quitGame = new Flag();
		private readonly Flag<bool> _gameRunning = new Flag<bool>();

		public int Coins => _metaModel.Coins;
		public int Crystals => _metaModel.Crystals;
		public float DeltaTime => _timeModel.DeltaTime;

		public IFlagHandle RequestMoreCoins => _requestMoreCoins;

		public IFlagHandle<int> ExchangeCoinsToCrystals => _exchangeCoinsToCrystals;

		public IFlag LayersChanged => _layersModel.LayersChanged;

		public IFlag<bool> GameRunning => _gameRunning;

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
			_simulationModel.Update(_timeModel.RealTimeSinceStartup);

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
				_layersModel.ShowViewOnTop((int) ViewResourceId.DisconnectedPopUpViewId);
			}

			if (_metaModel.IsConnected && _discPopupShown)
			{
				_discPopupShown = false;
				_layersModel.HideView((int) ViewResourceId.DisconnectedPopUpViewId);
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
			_layersModel.ShowViewOnTop((int) ViewResourceId.LobbyViewWindowId);
		}
		
		private void TransitFromLobbyToGame()
		{
			_currentState = ApplicationViewStates.Game;
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ViewResourceId.GameViewWindowId);
			
			_gameRunning.Raise(true);
			
			_simulationModel.InstantiatePrefab();
			_simulationModel.Show(_timeModel.RealTimeSinceStartup);
		}

		private void TransitFromGameToLobby()
		{
			_currentState = ApplicationViewStates.Lobby;
			
			_simulationModel.Hide();
			_simulationModel.DestroyInstanceForUnload();
			
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ViewResourceId.LobbyViewWindowId);
		}

		private void Init()
		{
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ViewResourceId.LoadingViewWindowId);
		}
		
		public enum ApplicationViewStates
		{
			Loading,
			Lobby,
			Game,
		}
	}
}
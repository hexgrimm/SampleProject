using Common;
using Models.Assets;
using Models.Layers;
using Models.Meta;
using Models.Simulation;
using Models.Times;

namespace Models.App
{
	public class ApplicationModel : ApplicationModelBase
	{
		public ApplicationModel(IMetaModel metaModel, ITimeModel timeModel, ILayersModel layersModel,
			ISimulationModel simulationModel, IAssetsModel assetsModel, IUpdateWatcher updateWatcher)
		{
			Data = new ApplicationModelSharedData();
			Data.AssetsModel = assetsModel;
			Data.LayersModel = layersModel;
			Data.MetaModel = metaModel;
			Data.SimulationModel = simulationModel;
			Data.TimeModel = timeModel;
			Data.UpdateWatcher = updateWatcher;
			
			BecomeContext();
			SetNewState(new LoadingState());
		}

		protected sealed override void SetNewState(ApplicationModelBase newState)
		{
			newState.Data = Data;
			base.SetNewState(newState);
		}

		public override void Update()
		{
			InnerState.Update();/*
			_currentState.
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
				GameUpdate();*/
		}

		public void StartNewGame()
		{
			//_startNewGame.Raise();
			InnerState.StartNewGame();
		}

		public void QuitGame()
		{
			//_quitGame.Raise();
			InnerState.QuitGame();
		}

		private void GameUpdate()
		{
			/*_metaModel.Update();
			
			if (_quitGame.Get)
			{
				TransitFromGameToLobby();
			}*/
		}

		private void LobbyUpdate()
		{
			/*_metaModel.Update();

			if (_requestMoreCoins.Get)
			{
				if (_metaModel.IsConnected)
				{
					_metaModel.RequestMoreCoins();
				}
			}

			if (_startNewGame.Get)
			{
				TransitFromLobbyToGame();
			}*/
		}

		private void LoadingUpdate()
		{
			/*_metaModel.Update();
			
			if (_currentState == ApplicationViewStates.Loading)
			{
				if (_metaModel.IsConnected)
				{
					TransitFromLoadingToLobby();
					return;
				}
			}*/
		}

		private void TransitFromLoadingToLobby()
		{
			/*_currentState = ApplicationViewStates.Lobby;
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ResourceId.LobbyWindow);*/
		}
		
		private void TransitFromLobbyToGame()
		{
			/*_currentState = ApplicationViewStates.Game;
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ResourceId.GameWindow);
			
			_gameRunning.Raise(true);
			
			//_simulationModel.InstantiatePrefab();
			_simulationModel.Show(_timeModel.RealTimeSinceStartup);*/
		}

		private void TransitFromGameToLobby()
		{
			/*_currentState = ApplicationViewStates.Lobby;
			
			_simulationModel.Hide();
			_simulationModel.DestroyInstance();
			
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ResourceId.LobbyWindow);*/
		}

		private void Init()
		{
			/*var asset = _assetsModel.LoadAsset(ResourceId.LoadingWindow);
			_layersModel.HideAll();
			_layersModel.ShowViewOnTop((int) ResourceId.LoadingWindow);*/
		}
	}
}
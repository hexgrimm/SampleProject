using Common;
using Models.Assets;
using UnityEngine;

namespace Models.App
{
	public class GameState : ApplicationModelBase
	{
		private readonly IPromise<GameObject> _gameWindowLoad;
		private readonly IPromise<GameObject> _gameSimLoad;
		private bool _loadComplete;
		
		public GameState(IPromise<GameObject> gameWindowLoad, IPromise<GameObject> gameSimLoad)
		{
			_gameWindowLoad = gameWindowLoad;
			_gameSimLoad = gameSimLoad;
		}

		public override void Update()
		{
			Data.UpdateWatcher.RegisterUpdate();
			Data.TimeModel.Update();
			Data.LayersModel.Update();
			Data.AssetsModel.Update();

			if (!_loadComplete && _gameSimLoad.IsCompleted && _gameWindowLoad.IsCompleted)
			{
				_loadComplete = true;
				Data.GameRunning.Raise(true);
				
				Data.LayersModel.HideView(ResourceId.LoadingWindow);
				Data.LayersModel.ShowViewOnTop(ResourceId.GameWindow, _gameWindowLoad.Result);
				
				Data.SimulationModel.InstantiatePrefab(_gameSimLoad.Result);
				Data.SimulationModel.Show(Data.TimeModel.RealTimeSinceStartup);
			}
			
			Data.SimulationModel.Update(Data.TimeModel.RealTimeSinceStartup);
			Data.MetaModel.Update();

			if (Data.QuitGame.Get)
			{
				SetNewState(new LoadingLobbyState());
				return;
			}
		}

		protected override void OnEnter()
		{
			Data.LayersModel.ShowViewOnTop(ResourceId.LoadingWindow, Data.AssetsModel.LoadAsset(ResourceId.LoadingWindow));
		}

		protected override void OnExit()
		{
			Data.LayersModel.HideAll();
			Data.SimulationModel.Hide();
			Data.SimulationModel.DestroyInstance();
		}
	}
}
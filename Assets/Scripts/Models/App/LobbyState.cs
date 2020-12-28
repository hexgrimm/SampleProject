using Common;
using Models.Assets;
using UnityEngine;

namespace Models.App
{
	public class LobbyState : ApplicationModelBase
	{
		private readonly IPromise<GameObject> _lobbyLoad;

		public LobbyState(IPromise<GameObject> lobbyLoad)
		{
			_lobbyLoad = lobbyLoad;
		}

		protected override void OnEnter()
		{
			Data.LayersModel.ShowViewOnTop(ResourceId.LobbyWindow, _lobbyLoad.Result);
		}

		protected override void OnExit()
		{
			Data.LayersModel.HideView(ResourceId.LobbyWindow);
		}

		public override void Update()
		{
			if (Data.RequestMoreCoins.Get)
			{
				if (Data.MetaModel.IsConnected)
				{
					Data.MetaModel.RequestMoreCoins();
				}
			}
			
			Data.UpdateWatcher.RegisterUpdate();
			Data.TimeModel.Update();
			Data.LayersModel.Update();
			Data.AssetsModel.Update();
			Data.SimulationModel.Update(Data.TimeModel.RealTimeSinceStartup);
			Data.MetaModel.Update();

			if (Data.StartNewGame.Get)
			{
				var gameWindowLoad = Data.AssetsModel.LoadAssetAsync(ResourceId.GameWindow);
				var gameSimLoad = Data.AssetsModel.LoadAssetAsync(ResourceId.SimulationPrefab);
				SetNewState(new GameState(gameWindowLoad, gameSimLoad));
			}
		}
	}
}
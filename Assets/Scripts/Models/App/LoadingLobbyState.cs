using System.Collections.Generic;
using System.Linq;
using Common;
using Models.Assets;
using UnityEngine;

namespace Models.App
{
	public class LoadingLobbyState : ApplicationModelBase
	{
		private IPromise<GameObject> _lobbyLoad;
		
		public override void Update()
		{
			Data.UpdateWatcher.RegisterUpdate();
			Data.TimeModel.Update();
			Data.LayersModel.Update();

			Data.AssetsModel.Update();
			Data.SimulationModel.Update(Data.TimeModel.RealTimeSinceStartup);
			Data.MetaModel.Update();
			
			if (Data.MetaModel.IsConnected && _lobbyLoad.IsCompleted)
			{
				SetNewState(new LobbyState(_lobbyLoad));
			}
		}

		protected override void OnEnter()
		{
			var loading = Data.AssetsModel.LoadAsset(ResourceId.LoadingWindow);
			Data.LayersModel.ShowViewOnTop(ResourceId.LoadingWindow, loading);
			
			_lobbyLoad = Data.AssetsModel.LoadAssetAsync(ResourceId.LobbyWindow);
		}

		protected override void OnExit()
		{
			Data.LayersModel.HideView(ResourceId.LoadingWindow);
		}
	}
}
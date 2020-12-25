using Common;
using Models.Assets;
using UnityEngine;

namespace Models.App
{
	public class LoadingState : ApplicationModelBase
	{
		private IPromise<GameObject> _loadingLoad;
		private bool _loadingShown = false;
		
		public override void Update()
		{
			Data.UpdateWatcher.RegisterUpdate();
			Data.TimeModel.Update();
			Data.LayersModel.Update();
			Data.AssetsModel.Update();
			Data.SimulationModel.Update(Data.TimeModel.RealTimeSinceStartup);
			Data.MetaModel.Update();

			if (_loadingLoad.IsCompleted)
			{
				
			}

			if (Data.MetaModel.IsConnected)
			{
				SetNewState(InnerState);
			}
		}

		protected override void OnEnter()
		{
			_loadingLoad = Data.AssetsModel.LoadAsset(ResourceId.LoadingWindow);
		}

		protected override void OnExit()
		{
			
		}

		private void ShowLoadIfNeeded()
		{
			if (_loadingShown)
				return;

			if (!_loadingLoad.IsCompleted)
				return;
			
			_loadingShown = true;
		}
	}
}
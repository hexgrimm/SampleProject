using Models.Assets;
using Models.Layers;
using Models.Meta;
using Models.Simulation;
using Models.Times;

namespace Models.App
{
	public sealed class ApplicationModel : ApplicationModelBase
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
			
			BecomeContext((x) => x.Data = Data);
			SetNewState(new LoadingLobbyState());
		}

		public override void Update()
		{
			InnerState.Update();
		}
	}
}
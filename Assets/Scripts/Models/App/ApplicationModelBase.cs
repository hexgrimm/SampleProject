using System.Collections.Generic;
using Common;
using GenericStates;
using Models.Assets;
using Models.Layers;
using Models.Meta;
using Models.Simulation;
using Models.Times;

namespace Models.App
{
	public abstract class ApplicationModelBase : State<ApplicationModelBase>, IApplicationModel
	{
		public ApplicationModelSharedData Data;
		
		public virtual int Coins => Data.MetaModel.Coins;
		public virtual int Crystals => Data.MetaModel.Crystals;
		public virtual float DeltaTime => Data.TimeModel.DeltaTime;
		public virtual IReadOnlyList<int> Layers => Data.LayersModel.Layers;
		public virtual IFlag LayersChanged => Data.LayersModel.LayersChanged;
		public virtual IFlag<bool> GameRunning => Data.GameRunning;

		public abstract void Update();
		
		public virtual void RequestMoreCoins()
		{
			Data.RequestMoreCoins.Raise();
		}

		public virtual void ExchangeCoinsToCrystals(int amount)
		{
			Data.ExchangeCoinsToCrystals.Raise(amount);
		}

		public void StartNewGame()
		{
			Data.StartNewGame.Raise();
		}

		public void QuitGame()
		{
			Data.QuitGame.Raise();
		}
		
		public class ApplicationModelSharedData
		{
			public readonly Flag RequestMoreCoins = new Flag();
			public readonly Flag<int> ExchangeCoinsToCrystals = new Flag<int>();
			public readonly Flag StartNewGame = new Flag();
			public readonly Flag QuitGame = new Flag();
			public readonly Flag<bool> GameRunning = new Flag<bool>();

			public IMetaModel MetaModel;
			public ITimeModel TimeModel;
			public ILayersModel LayersModel;
			public ISimulationModel SimulationModel;
			public IAssetsModel AssetsModel;
			public IUpdateWatcher UpdateWatcher;
		}
	}
}
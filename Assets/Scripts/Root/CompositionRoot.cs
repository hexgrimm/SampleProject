using System;
using System.Collections.Generic;
using Models;
using Models.ApplicationViewModel;
using Models.AssetsManagement;
using Models.Meta;
using Models.Simulation;
using Models.ViewLayersModel;
using Presenters;
using UnityEngine;
using Views;
using Time = Models.Time;

namespace Root
{
	public class CompositionRoot : MonoBehaviour, IUpdater
	{
		public event Action UpdateEvent = () => { };
		
		public AssetLinks AssetLinks;
		
		public Transform UiRoot;

		void Start()
		{
			BuildCodeTree();
		}

		private void BuildCodeTree()
		{
			List<IUpdateable> models = new List<IUpdateable>();
			
			var timeModel = new Time();
			models.Add(timeModel);

			var metaService = new MetaService(timeModel, 2);
			models.Add(metaService);

			var metaModel = new Meta(timeModel, metaService);
			models.Add(metaModel);
			
			var viewLayersModel = new ViewLayersModel();
			models.Add(viewLayersModel);

			var assetsModel = new AssetsModel(AssetLinks);
			var phys = new PhysicsSceneSimulation("Sim1");

			var simModel = new SimulationModel(phys, assetsModel);
			
			var appViewModel = new RootModel(metaModel, timeModel, viewLayersModel, simModel);
			models.Add(appViewModel);

			var presenters = new List<IUpdateablePresenter>();
			
			var lobbyView = new LobbyView(AssetLinks.LobbyWindowPrefab, UiRoot);
			presenters.Add(new LobbyViewPresenter(lobbyView, appViewModel));
			
			var loadingWindowView = new LoadingView(AssetLinks.LoadingWindowPrefab, UiRoot, this);
			presenters.Add(new LoadingPresenter(loadingWindowView, appViewModel));
			
			var gameWindowView = new GameWindow(AssetLinks.GameWindowPrefab, UiRoot);
			presenters.Add(new GameWindowPresenter(gameWindowView, appViewModel));
			
			
			
			var rootUpdater = new ApplicationLoop(models, presenters, this);
		}

		private void Update()
		{
			UpdateEvent();
		}
	}
}
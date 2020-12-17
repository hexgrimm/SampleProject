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
			//models
			var timeModel = new TimeModel(new UpdateWatcher("Time Model"));

			var metaService = new MetaService(timeModel, new UpdateWatcher("Meta Service"), 2f);

			var metaModel = new MetaModel(timeModel, metaService, new UpdateWatcher("Meta Model"));

			var viewLayersModel = new LayersModel(new UpdateWatcher("Layers Model"));

			var assetsModel = new AssetsModel(AssetLinks, new UpdateWatcher("Asset Model"));
			var phys = new PhysicsSceneSimulation("Sim1");

			var simModel = new SimulationModel(phys, assetsModel);
			
			var applicationModel = new ApplicationModel(metaModel, timeModel, viewLayersModel, simModel, assetsModel, new UpdateWatcher("Application Model"));

			var presenters = new List<IUpdateablePresenter>();
			
			//views
			var lobbyView = new LobbyView(AssetLinks.LobbyWindowPrefab, UiRoot);
			var loadingWindowView = new LoadingView(AssetLinks.LoadingWindowPrefab, UiRoot, this);
			var gameWindowView = new GameWindow(AssetLinks.GameWindowPrefab, UiRoot);
			
			//presenters
			presenters.Add(new LoadingPresenter(loadingWindowView, applicationModel));
			presenters.Add(new LobbyViewPresenter(lobbyView, applicationModel));
			presenters.Add(new GameWindowPresenter(gameWindowView, applicationModel));
			
			//root
			var rootUpdater = new ApplicationLoop(applicationModel, presenters, this);
		}

		private void Update()
		{
			UpdateEvent();
		}
	}
}
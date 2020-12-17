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
			var timeModel = new TimeModel(new UpdateWatcher());

			var metaService = new MetaService(timeModel, new UpdateWatcher(), 2f);

			var metaModel = new MetaModel(timeModel, metaService, new UpdateWatcher());

			var viewLayersModel = new ViewLayersModel(new UpdateWatcher());

			var assetsModel = new AssetsModel(AssetLinks, new UpdateWatcher());
			var phys = new PhysicsSceneSimulation("Sim1");

			var simModel = new SimulationModel(phys, assetsModel);
			
			var applicationModel = new ApplicationModel(metaModel, timeModel, viewLayersModel, simModel, new UpdateWatcher());

			var presenters = new List<IUpdateablePresenter>();
			
			var lobbyView = new LobbyView(AssetLinks.LobbyWindowPrefab, UiRoot);
			presenters.Add(new LobbyViewPresenter(lobbyView, applicationModel));
			
			var loadingWindowView = new LoadingView(AssetLinks.LoadingWindowPrefab, UiRoot, this);
			presenters.Add(new LoadingPresenter(loadingWindowView, applicationModel));
			
			var gameWindowView = new GameWindow(AssetLinks.GameWindowPrefab, UiRoot);
			presenters.Add(new GameWindowPresenter(gameWindowView, applicationModel));
			
			var rootUpdater = new ApplicationLoop(applicationModel, presenters, this);
		}

		private void Update()
		{
			UpdateEvent();
		}
	}
}
using System;
using System.Collections.Generic;
using Models;
using Models.App;
using Models.Assets;
using Models.Layers;
using Models.Meta;
using Models.Simulation;
using Models.Times;
using Presenters;
using Presenters.CoreLoop;
using UnityEngine;
using Views;

namespace Root
{
	public class CompositionRoot : MonoBehaviour
	{
		public AssetsConfiguration AssetsConfiguration;
		public Transform UiRoot;
		private ApplicationLoop _rootLoop;

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

			var assetsModel = new AssetsModel(AssetsConfiguration, new UpdateWatcher("Asset Model"));
			var phys = new Models.Simulation.PhysicsScene("Sim1");

			var simModel = new SimulationModel(phys, assetsModel);
			
			var applicationModel = new ApplicationModel(metaModel, timeModel, viewLayersModel, simModel, assetsModel, new UpdateWatcher("Application Model"));

			var presenters = new List<IUpdateablePresenter>();
			
			//views
			var lobbyView = new LobbyView(UiRoot);
			var loadingWindowView = new LoadingView(UiRoot);
			var gameWindowView = new GameWindow(UiRoot);
			
			//presenters
			presenters.Add(new LoadingWindowPresenter(loadingWindowView, applicationModel));
			presenters.Add(new LobbyWindowPresenter(lobbyView, applicationModel));
			presenters.Add(new GameWindowPresenter(gameWindowView, applicationModel));
			
			//root
			_rootLoop = new ApplicationLoop(applicationModel, presenters);
		}

		private void Update()
		{
			_rootLoop.Update();
		}
	}
}
using System;
using System.Collections.Generic;
using Models;
using Models.ApplicationViewModel;
using Models.Meta;
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

			var appViewModel = new ApplicationViewModel(metaModel, timeModel, viewLayersModel);
			models.Add(appViewModel);

			var presenters = new List<IUpdateablePresenter>();
			
			var lobbyView = new LobbyView(AssetLinks.LobbyWindowPrefab, UiRoot);
			presenters.Add(new LobbyViewPresenter(lobbyView, appViewModel));
			
			var loadingWindowView = new LoadingView(AssetLinks.LoadingWindowPrefab, UiRoot, this);
			presenters.Add(new LoadingPresenter(loadingWindowView, appViewModel));
			
			
			
			var rootUpdater = new ApplicationLoop(models, presenters, this);
		}

		private void Update()
		{
			UpdateEvent();
		}
	}
}
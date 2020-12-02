using System;
using System.Collections.Generic;
using Models;
using Models.Meta;
using Presenters;
using UnityEngine;
using Views;

namespace Root
{
	public class CompositionRoot : MonoBehaviour, IUpdater
	{
		public event Action UpdateEvent = () => { };
		
		//just for example. This can be injected as resource database or smth to views directly.
		public GameObject LoadingPrefab;
		public GameObject LobbyPrefab;
		public Transform UiRoot;

		void Start()
		{
			BuildCodeTree();
		}

		private void BuildCodeTree()
		{
			List<IUpdateableModel> models = new List<IUpdateableModel>();
			
			var loadingWindowView = new LoadingView(LoadingPrefab, UiRoot, this);
			var lobbyView = new LobbyView(LobbyPrefab, UiRoot);
			
			var timeModel = new TimeModel();
			var appInitModel = new AppInitModel(timeModel);
			var metaConnectionModel = new MetaConnectionModel(timeModel, 2);
			var playerBalanceModel = new MetaModel(timeModel, metaConnectionModel);
			
			var presenterStateFactory = new PresenterStateFactory(loadingWindowView, appInitModel, lobbyView, playerBalanceModel, timeModel);
			
			models.Add(timeModel);
			models.Add(appInitModel);
			models.Add(metaConnectionModel);
			models.Add(playerBalanceModel);
			
			var modelUpdaterInOrder = new ModelUpdaterInOrder(models);
			var presenter = new RootPresenter(modelUpdaterInOrder, presenterStateFactory, this);
		}

		private void Update()
		{
			UpdateEvent();
		}
	}
}
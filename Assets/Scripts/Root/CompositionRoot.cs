using System;
using Models;
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
			//all this can be done by DI container. Or not. I wrote it without once, worked fine too.
			
			var loadingWindowView = new LoadingView(LoadingPrefab, UiRoot, this);
			var lobbyView = new LobbyView(LobbyPrefab, UiRoot);
			
			var timeModel = new TimeModel();
			var appInitModel = new AppInitModel(timeModel);
			var playerBalanceModel = new PlayerBalanceModel(timeModel);
			
			var presenterStateFactory = new PresenterStateFactory(loadingWindowView, appInitModel, lobbyView, playerBalanceModel, timeModel);
			
			var rootModel = new RootModel(appInitModel, timeModel, playerBalanceModel);
			var presenter = new RootPresenter(rootModel, presenterStateFactory, this);
		}

		private void Update()
		{
			UpdateEvent();
		}
	}
}
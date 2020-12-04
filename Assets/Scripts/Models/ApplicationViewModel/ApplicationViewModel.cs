using Models.ViewLayersModel;

namespace Models.ApplicationViewModel
{
	//TODO: convert it into a context for state pattern
	public class ApplicationViewModel : IUpdateableModel
	{
		private readonly IMetaModel _metaModel;
		private readonly ITimeModel _timeModel;
		private readonly IViewLayersModel _viewLayersModel;
		
		private bool _isInitialized = false;
		private bool _discPopupShown;

		public ApplicationViewStates CurrentState { get; private set; }

		public int Coins => _metaModel.Coins;

		public int Crystals => _metaModel.Crystals;

		public ApplicationViewModel(IMetaModel metaModel, ITimeModel timeModel, IViewLayersModel viewLayersModel)
		{
			_metaModel = metaModel;
			_timeModel = timeModel;
			_viewLayersModel = viewLayersModel;
			CurrentState = ApplicationViewStates.Loading;
		}
		
		public void Update()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				Init();
			}

			if (CurrentState == ApplicationViewStates.Loading)
				LoadingUpdate();
			else if (CurrentState == ApplicationViewStates.Lobby)
				LobbyUpdate();
		}

		private void LobbyUpdate()
		{
			if (!_metaModel.IsConnected && !_discPopupShown)
			{
				_discPopupShown = true;
				_viewLayersModel.ShowViewOnTop(ViewsConfiguration.DisconnectedPopUpViewId);
			}

			if (_metaModel.IsConnected && _discPopupShown)
			{
				_discPopupShown = false;
				_viewLayersModel.HideView(ViewsConfiguration.DisconnectedPopUpViewId);
			}
		}

		private void LoadingUpdate()
		{
			
			if (CurrentState == ApplicationViewStates.Loading)
			{
				if (_metaModel.IsConnected)
				{
					TransitFromLoadingToLobby();
					return;
				}
			}
		}

		private void TransitFromLoadingToLobby()
		{
			CurrentState = ApplicationViewStates.Lobby;
			_viewLayersModel.HideAll();
			_viewLayersModel.ShowViewOnTop(ViewsConfiguration.LobbyViewId);
		}

		private void Init()
		{
			_viewLayersModel.HideAll();
			_viewLayersModel.ShowViewOnTop(ViewsConfiguration.LoadingViewId);
		}


		public enum ApplicationViewStates
		{
			Loading,
			Lobby,
		}
	}
}
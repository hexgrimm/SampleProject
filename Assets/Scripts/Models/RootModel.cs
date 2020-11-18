namespace Models
{
	public class RootModel : IRootModel
	{
		private readonly IUpdateableModel _appInitModel;
		private readonly IUpdateableModel _timeModel;
		private readonly IPlayerBalanceModel _playerBalanceModel;

		public RootModel(IUpdateableModel appInitModel, IUpdateableModel timeModel, IPlayerBalanceModel playerBalanceModel)
		{
			_appInitModel = appInitModel;
			_timeModel = timeModel;
			_playerBalanceModel = playerBalanceModel;
		}
		
		public void Update()
		{
			//control of update order between models to evade skipping frames
			_timeModel.Update();
			_appInitModel.Update();
		}
	}
}
namespace Models
{
	public class PlayerBalanceModel : IPlayerBalanceModel, IUpdateableModel
	{
		private readonly ITimeModel _timeModel;

		public PlayerBalanceModel(ITimeModel timeModel)
		{
			_timeModel = timeModel;
		}

		public void Update()
		{
			
		}
	}
}
using System;

namespace Models
{
	public class PlayerBalanceModel : IPlayerBalanceModel, IUpdateableModel
	{
		private readonly ITimeModel _timeModel;
		public event Action CoinsValueChanged = () => { };

		private float _requestTimer = -1;
		private bool _coinsRequested;
		
		public PlayerBalanceModel(ITimeModel timeModel)
		{
			_timeModel = timeModel;
		}

		public int Coins { get; private set; } = 42;

		public void Update()
		{
			_requestTimer -= _timeModel.DeltaTime;
			
			if (_coinsRequested && _requestTimer < 0)
			{
				AddCoins();
				_coinsRequested = false;
			}
		}

		public void RequestMoreCoins()
		{
			_coinsRequested = true;
			_requestTimer = 0.5f;
		}

		private void AddCoins()
		{
			Coins += 42;
			CoinsValueChanged();
		}
	}
}
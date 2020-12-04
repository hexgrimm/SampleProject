using System;
using System.Collections.Generic;
using EventUtils;

namespace Models.Meta
{
	public class Meta : IMetaModel, IUpdateable
	{
		private const float NetworkDelay = 0.42f;
		private readonly ITimeModel _timeModel;
		private readonly IMetaService _metaService;

		public int Coins { get; private set; } = 100;
		
		public int Crystals { get; private set; }
		
		public bool IsConnected { get; private set; }

		private readonly List<(float requestTime, Promise promise, Action action)> _requests = 
			new List<(float requestTime, Promise promise, Action action)>();
		
		public Meta(ITimeModel timeModel, IMetaService metaService)
		{
			_timeModel = timeModel;
			_metaService = metaService;
		}
		
		public IPromise RequestMoreCoins()
		{
			var promise = new Promise();
			
			_requests.Add((_timeModel.RealTimeSinceStartup, promise, () =>
			{
				if (IsConnected)
				{
					AddCoins();
					promise.SetComplete();
				}
				else
				{
					promise.SetFailed();
				}
			}));
			return promise;
		}

		public IPromise ExchangeCoinsToCrystals(int coinsAmount)
		{
			var promise = new Promise();
			_requests.Add((_timeModel.RealTimeSinceStartup, promise, () =>
			{
				if (IsConnected)
				{
					ExchangeCoins(coinsAmount, promise);
					promise.SetComplete();
				}
				else
				{
					promise.SetFailed();
				}
			}));
			return promise;
		}

		public void Update()
		{
			IsConnected = _metaService.IsConnected;
			
			for (int i = _requests.Count - 1; i >= 0; i--)
			{
				var item = _requests[i];
				if (item.requestTime + NetworkDelay < _timeModel.RealTimeSinceStartup)
				{
					_requests.RemoveAt(i);
					item.action();
				}
			}
		}

		private void ExchangeCoins(int coinsAmount, Promise promise)
		{
			if (Coins >= coinsAmount)
			{
				Coins -= coinsAmount;
				Crystals += coinsAmount;
				promise.SetComplete();
			}
			else
			{
				promise.SetFailed();
			}
		}

		private void AddCoins()
		{
			Coins += 34;
		}
	}
}
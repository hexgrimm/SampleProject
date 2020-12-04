using UnityEngine;

namespace Models.Meta
{
	public class MetaService : IMetaService, IUpdateable
	{
		private readonly ITimeModel _timeModel;
		private float? _emulateConnectDelay;
		private readonly bool _emulateDisconnects;

		public bool IsConnected { get; private set; } = false;

		private float _initTime;
		private float _nextDiscTime;
		private float _discDuration = 2f;
		
		public MetaService(ITimeModel timeModel, float? emulateConnectDelay = null, bool emulateDisconnects = false)
		{
			_timeModel = timeModel;
			_emulateConnectDelay = emulateConnectDelay;
			_emulateDisconnects = emulateDisconnects;
			_initTime = _timeModel.RealTimeSinceStartup;

			float startDelay = emulateConnectDelay ?? 0;
			_nextDiscTime = _timeModel.RealTimeSinceStartup + startDelay + Random.Range(3f, 7f);
		}
		
		public void Update()
		{
			if (_emulateConnectDelay.HasValue)
			{
				if (_initTime + _emulateConnectDelay.Value < _timeModel.RealTimeSinceStartup)
				{
					_emulateConnectDelay = null;
					IsConnected = true;
				}
				
				return;
			}

			if (_emulateDisconnects && !_emulateConnectDelay.HasValue)
			{
				if (_nextDiscTime > _timeModel.RealTimeSinceStartup)
				{
					IsConnected = true;
					return;
				}

				if (_nextDiscTime + _discDuration < _timeModel.RealTimeSinceStartup)
				{
					IsConnected = true;
					return;
				}

				IsConnected = false;
			}
		}
	}
}
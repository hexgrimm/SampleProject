using Models.Time;
using UnityEngine;

namespace Models.Meta
{
	public class MetaService : IMetaService, IUpdateable
	{
		private readonly ITimeModel _timeModel;
		private readonly IUpdateWatcher _updateWatcher;
		private float? _emulateConnectDelay;
		private readonly bool _emulateDisconnects;

		public bool IsConnected { get; private set; } = false;

		private readonly float _initTime;
		private readonly float _nextDiscTime;
		private float _discDuration = 2f;
		
		public MetaService(ITimeModel timeModel, IUpdateWatcher updateWatcher, float? emulateConnectDelay = null, bool emulateDisconnects = false)
		{
			_timeModel = timeModel;
			_updateWatcher = updateWatcher;
			_emulateConnectDelay = emulateConnectDelay;
			_emulateDisconnects = emulateDisconnects;
			_initTime = _timeModel.RealTimeSinceStartup;

			float startDelay = emulateConnectDelay ?? 0;
			_nextDiscTime = _timeModel.RealTimeSinceStartup + startDelay + Random.Range(3f, 7f);
		}
		
		public void Update()
		{
			_updateWatcher.RegisterUpdate();
			
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
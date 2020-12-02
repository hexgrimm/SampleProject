namespace Models.Meta
{
	public class MetaConnectionModel : IMetaConnectionModel, IUpdateableModel
	{
		private readonly ITimeModel _timeModel;
		private float? _emulateConnectDelay;

		public bool IsConnected { get; private set; } = false;

		private float _initTime;
		
		public MetaConnectionModel(ITimeModel timeModel, float? emulateConnectDelay = null)
		{
			_timeModel = timeModel;
			_emulateConnectDelay = emulateConnectDelay;
			_initTime = _timeModel.RealTimeSinceStartup;
		}
		
		public void Update()
		{
			if (_emulateConnectDelay.HasValue &&
			    _initTime + _emulateConnectDelay.Value < _timeModel.RealTimeSinceStartup)
			{
				_emulateConnectDelay = null;
				IsConnected = true;
			}
		}
	}
}
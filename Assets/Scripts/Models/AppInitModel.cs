using System;

namespace Models
{
	public class AppInitModel : IAppInitModel, IUpdateableModel
	{
		private readonly ITimeModel _timeModel;
		public event Action AllPluginsInitialized = () => { };

		private float _timer = 4f;
		public AppInitModel(ITimeModel timeModel)
		{
			_timeModel = timeModel;
		}

		public void Update()
		{
			_timer -= _timeModel.DeltaTime;

			if (_timer < 0)
			{
				_timer = float.MaxValue;
				AllPluginsInitialized();
			}
		}
	}
}
namespace Models.Time
{
	public class TimeModel : ITimeModel
	{
		private readonly IUpdateWatcher _updateWatcher;
		public float DeltaTime { get; private set; }
		public float RealTimeSinceStartup { get; private set; }
		
		
		public TimeModel(IUpdateWatcher updateWatcher)
		{
			_updateWatcher = updateWatcher;
		}
		
		public void Update()
		{
			_updateWatcher.RegisterUpdate();
			
			DeltaTime = UnityEngine.Time.deltaTime;
			RealTimeSinceStartup = UnityEngine.Time.realtimeSinceStartup;
		}
	}
}
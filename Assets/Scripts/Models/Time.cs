using UnityEngine;

namespace Models
{
	public class Time : ITimeModel, IUpdateable
	{
		public float DeltaTime { get; private set; }
		public float RealTimeSinceStartup { get; private set; }

		public void Update()
		{
			DeltaTime = UnityEngine.Time.deltaTime;
			RealTimeSinceStartup = UnityEngine.Time.realtimeSinceStartup;
		}
	}
}
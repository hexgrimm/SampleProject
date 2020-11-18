using UnityEngine;

namespace Models
{
	public class TimeModel : ITimeModel, IUpdateableModel
	{
		public float DeltaTime { get; private set; }
		public float RealTimeSinceStartup { get; private set; }

		public void Update()
		{
			DeltaTime = Time.deltaTime;
			RealTimeSinceStartup = Time.realtimeSinceStartup;
		}
	}
}
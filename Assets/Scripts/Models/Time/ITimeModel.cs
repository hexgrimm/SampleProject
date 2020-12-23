namespace Models.Time
{
	public interface ITimeModel : IUpdateable
	{
		float DeltaTime { get; }
		float RealTimeSinceStartup { get; }
	}
}
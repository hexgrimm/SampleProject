namespace Models.Times
{
	public interface ITimeModel : IUpdateable
	{
		float DeltaTime { get; }
		float RealTimeSinceStartup { get; }
	}
}
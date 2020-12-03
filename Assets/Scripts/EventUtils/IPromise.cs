namespace EventUtils
{
	public interface IPromise
	{
		bool IsCompleted { get; }
		bool IsFaulted { get; }
	}
}
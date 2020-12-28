namespace Common
{
	public interface IPromise
	{
		bool IsCompleted { get; }
		bool IsFaulted { get; }
	}
	
	public interface IPromise<T>
	{
		T Result { get; }
		bool IsCompleted { get; }
		bool IsFaulted { get; }
	}
}
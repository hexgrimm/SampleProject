namespace Models
{
	public interface IPromise
	{
		bool IsCompleted { get; }
		bool IsFaulted { get; }

		void Complete(bool withNoError = true);
	}
}
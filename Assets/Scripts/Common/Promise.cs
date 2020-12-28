namespace Common
{
	public class Promise : IPromise
	{
		public bool IsCompleted { get; private set; }
		public bool IsFaulted { get; private set; }
		
		public void SetComplete()
		{
			IsCompleted = true;
			IsFaulted = false;
		}

		public void SetFailed()
		{
			IsCompleted = true;
			IsFaulted = true;
		}
	}

	public class Promise<T> : IPromise<T>
	{
		public T Result  { get; private set; }
		public bool IsCompleted { get; private set; }
		public bool IsFaulted { get; private set; }
		
		public void SetComplete(T result)
		{
			IsCompleted = true;
			IsFaulted = false;
			Result = result;
		}

		public void SetFailed()
		{
			IsCompleted = true;
			IsFaulted = true;
		}
	}
}
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
}
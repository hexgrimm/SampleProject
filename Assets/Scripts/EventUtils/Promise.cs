namespace Models
{
	public class Promise : IPromise
	{
		public bool IsCompleted { get; private set; }
		public bool IsFaulted { get; private set; }
		
		public void Complete(bool withNoError = true)
		{
			IsCompleted = true;
			IsFaulted = !withNoError;
		}
	}
}
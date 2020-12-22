namespace EventUtils
{
	public interface ISignal
	{
		bool Get { get; }
	}
	
	public interface ISignal<T> where T : struct
	{
		T? Get { get; }
	}
}
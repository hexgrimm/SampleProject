namespace EventUtils
{
	public interface IFlag
	{
		bool Get { get; }
	}
	
	public interface IFlag<T> where T : struct
	{
		T? Get { get; }
	}
}
namespace EventUtils
{
	public interface IFlagHandle
	{
		void Raise();
	}
	public interface IFlagHandle<T> where T : struct
	{
		void Raise(T value);
	}
}
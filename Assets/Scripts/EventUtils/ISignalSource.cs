namespace EventUtils
{
	public interface ISignalSource
	{
		void Raise();
	}
	public interface ISignalSource<T> where T : struct
	{
		void Raise(T value);
	}
}
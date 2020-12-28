using UnityEngine;

namespace Common
{
	public class Flag : IFlagHandle, IFlag
	{
		private int _frameOfRaise = -1;
		
		public bool Get => Time.frameCount == _frameOfRaise;

		public void Raise()
		{
			_frameOfRaise = Time.frameCount;
		}
	}
	
	public class Flag<T> : IFlagHandle<T>, IFlag<T> where T : struct
	{
		private int _frameOfRaise = -1;

		private T _value;
		
		public T? Get
		{
			get { return Time.frameCount == _frameOfRaise ? default : _value; }
		}

		public void Raise(T value)
		{
			_frameOfRaise = Time.frameCount;
			_value = value;
		}
	}
}
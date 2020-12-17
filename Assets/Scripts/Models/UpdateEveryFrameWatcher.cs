using UnityEngine;

namespace Models
{
	public interface IUpdateWatcher
	{
		void RegisterUpdate();
	}

	public class UpdateWatcher : IUpdateWatcher
	{
		private int _lastFrameOfUpdate;

		public void RegisterUpdate()
		{
			if (_lastFrameOfUpdate + 1 != Time.frameCount)
			{
				Debug.LogError("Update of model is missed");
			}
			else if (_lastFrameOfUpdate == Time.frameCount)
			{
				Debug.LogError("Model update was called multiple times");
			}

			_lastFrameOfUpdate = Time.frameCount;
		}
	}
}
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
	public class UpdateWatcher : IUpdateWatcher
	{
		private int _lastFrameOfUpdate;

		private static readonly Dictionary<UpdateWatcher, int> Watchers = new Dictionary<UpdateWatcher, int>();
		private static int _lastOverallCheckFrame;

		public string DebugName;
		
		public UpdateWatcher(string debugName)
		{
			DebugName = debugName;
			Watchers.Add(this, Time.frameCount);
		}
		
		public void RegisterUpdate()
		{
			var currentFrame = Time.frameCount;
			
			if (_lastOverallCheckFrame != currentFrame)
			{
				_lastOverallCheckFrame = currentFrame;

				foreach (KeyValuePair<UpdateWatcher,int> keyValuePair in Watchers)
				{
					if (keyValuePair.Value + 1 < currentFrame)
					{
						Debug.LogError("Missed calls for watcher " + keyValuePair.Key.DebugName);
					}
				}
			}
			
			if (_lastFrameOfUpdate + 1 != currentFrame)
			{
				Debug.LogError("Update of model is missed");
			}
			else if (_lastFrameOfUpdate == currentFrame)
			{
				Debug.LogError("Model update was called multiple times");
			}

			_lastFrameOfUpdate = currentFrame;
			Watchers[this] = _lastFrameOfUpdate;
		}
	}
}
using System;

namespace EventUtils
{
	public class EventDelay //used to reorder callback execution inside subscriber 
	{
		private Action _action;
		private bool _eventRaised;
		
		public void SetCallback(Action action)
		{
			_action = action;
		}

		public void MethodForDirectSubscribing()
		{
			_eventRaised = true;
		}

		public void PollChanges()
		{
			if (_eventRaised)
			{
				_eventRaised = false;
				_action?.Invoke();
			}
		}

		public void ClearCalls()
		{
			_eventRaised = false;
		}
	}
}
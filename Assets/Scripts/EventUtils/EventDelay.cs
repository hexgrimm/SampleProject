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

		public void DirectMethod()
		{
			_eventRaised = true;
		}

		public void ProceedReactionIfNeeded()
		{
			if (_eventRaised)
			{
				_eventRaised = false;
				_action?.Invoke();
			}
		}
	}
}
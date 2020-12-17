using System;

namespace Views
{
	public interface IUpdater
	{
		event Action UpdateEvent;
		//TODO: OnAppPause and etc...
	}
}
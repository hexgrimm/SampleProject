using System;

namespace Views
{
	public interface IUpdater
	{
		event Action UpdateEvent;
		//OnAppPause and etc...
	}
}
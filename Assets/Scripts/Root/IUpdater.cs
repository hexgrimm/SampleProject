using System;

namespace Root
{
	public interface IUpdater
	{
		event Action UpdateEvent;
		//OnAppPause and etc...
	}
}
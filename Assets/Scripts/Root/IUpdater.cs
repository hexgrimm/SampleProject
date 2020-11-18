using System;

namespace Root
{
	public interface IUpdater
	{
		event Action Update;
		//OnAppPause and etc...
	}
}
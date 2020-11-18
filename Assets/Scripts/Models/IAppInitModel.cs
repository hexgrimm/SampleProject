using System;

namespace Presenters
{
	//example with event
	public interface IAppInitModel
	{
		event Action AllPluginsInitialized;
	}
}
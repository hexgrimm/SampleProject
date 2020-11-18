using System;

namespace Models
{
	//example with event
	public interface IAppInitModel
	{
		event Action AllPluginsInitialized;
	}
}
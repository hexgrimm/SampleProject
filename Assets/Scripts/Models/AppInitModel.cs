using System;
using System.Threading;
using System.Threading.Tasks;
using Models;

namespace Presenters
{
	public class AppInitModel : IAppInitModel, IUpdateableModel
	{
		private readonly ITimeModel _timeModel;
		public event Action AllPluginsInitialized = () => { };

		public AppInitModel(ITimeModel timeModel)
		{
			_timeModel = timeModel;
			var task = Task.Run(() =>
			{
				Thread.Sleep(7000);
				AllPluginsInitialized();
			});
		}

		public void Update()
		{
			
		}
	}
}
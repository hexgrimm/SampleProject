using Models;
using Models.ApplicationViewModel;
using Views.SimulationVIew;

namespace Presenters
{
	public class SimulationPresenter : IUpdateablePresenter
	{
		private readonly ISimulationModel _view;
		private readonly IAssetsModel _assetsModel;
		private readonly IApplicationViewModel _applicationViewModel;

		public SimulationPresenter(ISimulationModel view, IAssetsModel assetsModel, IApplicationViewModel applicationViewModel)
		{
			_view = view;
			_assetsModel = assetsModel;
			_applicationViewModel = applicationViewModel;
		}

		public void PreModelUpdate()
		{
			_view.SimulatePhysics(1);
		}

		public void PostModelUpdate()
		{
			var gameStateChanged = _applicationViewModel.GameRunning.Get;
			if (gameStateChanged.HasValue)
			{
				if (gameStateChanged.Value)
				{
					_view.Show();
				}
			}
		}
	}
}
using System.Collections.Generic;
using Models;

namespace Presenters.CoreLoop
{	//General orchestrator of an application execution flow. Fully non-Unity
	public class ApplicationLoop
	{
		private readonly IUpdateable _applicationModel;
		private readonly IList<IUpdateablePresenter> _presentersInOrder;

		public ApplicationLoop(IUpdateable applicationModel, IList<IUpdateablePresenter> presentersInOrder)
		{
			_applicationModel = applicationModel;
			_presentersInOrder = presentersInOrder;
		}

		public void Update()
		{
			foreach (var updateablePresenter in _presentersInOrder)
			{
				updateablePresenter.PreModelUpdate();
			}

			_applicationModel.Update();

			foreach (var updateablePresenter in _presentersInOrder)
			{
				updateablePresenter.PostModelUpdate();
			}
		}
	}
}
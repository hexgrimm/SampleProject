using System.Collections.Generic;
using Models;
using UnityEngine;
using Views;

namespace Presenters
{	//General orchestrator of an application execution flow. Fully non-Unity
	public class ApplicationLoop
	{
		private readonly IUpdateable _applicationModel;
		private readonly IList<IUpdateablePresenter> _presentersInOrder;
		private readonly IUpdater _updater;

		public ApplicationLoop(IUpdateable applicationModel, IList<IUpdateablePresenter> presentersInOrder, IUpdater updater)
		{
			_applicationModel = applicationModel;
			_presentersInOrder = presentersInOrder;
			updater.UpdateEvent += Update;
		}

		private void Update()
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
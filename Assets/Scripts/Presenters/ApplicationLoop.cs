using System.Collections.Generic;
using Models;
using UnityEngine;
using Views;

namespace Presenters
{	//General orchestrator of an application execution flow. Fully non-Unity
	public class ApplicationLoop
	{
		private readonly IList<IUpdateable> _modelsInOrder;
		private readonly IList<IUpdateablePresenter> _presentersInOrder;
		private readonly IUpdater _updater;

		public ApplicationLoop(IList<IUpdateable> modelsInOrder, IList<IUpdateablePresenter> presentersInOrder, IUpdater updater)
		{
			_modelsInOrder = modelsInOrder;
			_presentersInOrder = presentersInOrder;
			updater.UpdateEvent += Update;
		}

		private void Update()
		{
			foreach (var updateablePresenter in _presentersInOrder)
			{
				updateablePresenter.PreModelUpdate();
			}
			
			foreach (var updateableModel in _modelsInOrder)
			{
				updateableModel.Update();
			}
			
			foreach (var updateablePresenter in _presentersInOrder)
			{
				updateablePresenter.PostModelUpdate();
			}
		}
	}
}
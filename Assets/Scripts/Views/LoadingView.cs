using System;
using Root;
using UnityEngine;

namespace Views
{
	public class LoadingView : ViewBase<LoadingViewPrefabLinks>, ILoadingWindowView
	{
		private readonly IUpdater _updater;
		private bool isRotating;
		
		public LoadingView(GameObject prefab, Transform parent, IUpdater updater) : base(prefab, parent)
		{
			_updater = updater;
		}

		public void RotateSpinner()
		{
			//better use dotween or start animation. I injected updater just as shortcut

			if (!isRotating)
			{
				isRotating = true;
				_updater.UpdateEvent += RotateSpinnerMethod;
			}
		}

		private void RotateSpinnerMethod()
		{
			PrefabLink.Spinner.transform.Rotate(0, 0, 90 * Time.deltaTime);
		}

		public override void Hide()
		{
			if (isRotating)
			{
				_updater.UpdateEvent -= RotateSpinnerMethod;
				isRotating = false;
			}
			
			base.Hide();
		}
	}
}
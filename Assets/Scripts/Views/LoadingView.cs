using UnityEngine;

namespace Views
{
	public class LoadingView : ViewBase<LoadingViewPrefabLinks>, ILoadingWindowView
	{
		private readonly IUpdater _updater;
		private bool _isRotating;
		
		public LoadingView(GameObject prefab, Transform parent, IUpdater updater) : base(prefab, parent)
		{
			_updater = updater;
		}

		public void EnableSpinnerRotation()
		{
			//better use dotween or start animation. I injected updater just as shortcut

			if (!_isRotating)
			{
				_isRotating = true;
				_updater.UpdateEvent += RotateSpinnerMethod;
			}
		}

		public void ShowOnLayer(int layerIndex)
		{
			base.Instantiate();
			GameObjectInstance.transform.SetSiblingIndex(layerIndex);
		}

		private void RotateSpinnerMethod()
		{
			PrefabLink.Spinner.transform.Rotate(0, 0, 90 * Time.deltaTime);
		}

		public override void Hide()
		{
			if (_isRotating)
			{
				_updater.UpdateEvent -= RotateSpinnerMethod;
				_isRotating = false;
			}
			
			base.Hide();
		}
	}
}
using UnityEngine;

namespace Views
{
	public interface ILoadingWindowView
	{
		void EnableSpinnerRotation();
		void ShowOnLayer(int layerIndex, GameObject prefab);
		void Hide();
	}
}
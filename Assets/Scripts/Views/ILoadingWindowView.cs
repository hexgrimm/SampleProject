using UnityEngine;

namespace Views
{
	public interface ILoadingWindowView
	{
		void ShowOnLayer(int layerIndex, GameObject prefab);
		void Hide();
	}
}
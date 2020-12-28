using UnityEngine;

namespace Views
{
	public interface IWindow
	{
		void ShowOnLayer(int layerIndex, GameObject prefab);
		void Hide();
	}
}
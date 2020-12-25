using Common;
using UnityEngine;

namespace Views
{
	public interface IGameWindow
	{
		IFlag QuitGameButton { get; }
		void ShowOnLayer(int layerIndex, GameObject prefab);
		void Hide();
	}
}
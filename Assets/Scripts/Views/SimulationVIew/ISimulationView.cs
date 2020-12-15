using UnityEngine;

namespace Views.SimulationVIew
{
	public interface ISimulationView
	{
		void InstantiatePrefab(GameObject prefab);
		void Show();
		void Hide();
		void DestroyInstanceForUnload();
		void SimulatePhysics(float deltaTime);
	}
}
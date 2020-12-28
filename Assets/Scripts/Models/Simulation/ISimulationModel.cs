using UnityEngine;

namespace Models.Simulation
{
	public interface ISimulationModel
	{
		void InstantiatePrefab(GameObject prefab);
		void Show(float startTime);
		void Hide();
		void DestroyInstance();
		void Update(float toTime);
	}
}
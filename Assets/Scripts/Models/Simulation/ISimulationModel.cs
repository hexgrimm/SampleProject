using UnityEngine;

namespace Views.SimulationVIew
{
	public interface ISimulationModel
	{
		void InstantiatePrefab();
		void Show();
		void Hide();
		void DestroyInstanceForUnload();
		void Update(float deltaTime);
	}
}
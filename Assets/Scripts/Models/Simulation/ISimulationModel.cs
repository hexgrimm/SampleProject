namespace Models.Simulation
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
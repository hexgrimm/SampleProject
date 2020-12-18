namespace Models.Simulation
{
	public interface ISimulationModel
	{
		void InstantiatePrefab();
		void Show(float startTime);
		void Hide();
		void DestroyInstanceForUnload();
		void Update(float toTime);
	}
}
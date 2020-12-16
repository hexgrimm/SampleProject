using UnityEngine;
using Views.SimulationVIew;

namespace Models.Simulation
{
	public class SimulationModel : ISimulationModel
	{
		private readonly IPhysicsSceneSimulation _physicsSceneSimulation;
		private readonly IAssetsModel _assetsModel;

		private GameObject _instance;
		private GameObject _prefab;

		public SimulationModel(IPhysicsSceneSimulation physicsSceneSimulation, IAssetsModel assetsModel)
		{
			_physicsSceneSimulation = physicsSceneSimulation;
			_assetsModel = assetsModel;
		}

		public void InstantiatePrefab()
		{
			_prefab = _assetsModel.LoadAsset(_assetsModel.Links.SimulationPrefabLink.Id);
			
			if (_instance != null)
			{
				Object.Destroy(_instance);
				Debug.LogWarning("Loading prefab duplicate");
			}
			
			_instance = Object.Instantiate(_prefab, _physicsSceneSimulation.RootTransform);
			_instance.SetActive(false);
		}

		public void Show()
		{
			if (_instance != null)
				_instance.SetActive(true);
		}

		public void Hide()
		{
			if (_instance != null)
				_instance.SetActive(false);
		}

		public void DestroyInstanceForUnload()
		{
			GameObject.Destroy(_instance);
		}

		public void SimulatePhysics(float deltaTime)
		{
			_physicsSceneSimulation.SimulatePhysics(deltaTime);
		}
	}

	public interface IPhysicsSceneSimulation
	{
		void SimulatePhysics(float deltaTime);
		Transform RootTransform { get; }
	}
}

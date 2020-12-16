using GameSimService;
using UnityEngine;
using Views.SimulationVIew;

namespace Models.Simulation
{
	public class SimulationModel : ISimulationModel
	{
		private readonly IPhysicsSceneSimulation _physicsSceneSimulation;
		private readonly IAssetsModel _assetsModel;

		private GameObject _instance;
		private SimulationLinks _simulationLinks;
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
			_simulationLinks = _instance.GetComponent<SimulationLinks>();
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
			_simulationLinks = null;
			GameObject.Destroy(_instance);
		}

		public void Update(float deltaTime)
		{
			const float fixedTime = 0.003f;

			int fullIterations = (int) (deltaTime / fixedTime);
			float addition = deltaTime - (fullIterations * fixedTime);
			
			for (int i = 0; i < fullIterations; i++)
			{
				_simulationLinks.KnifeMono.UpdateObject(fixedTime);
				_physicsSceneSimulation.SimulatePhysics(fixedTime);
			}
			
			_simulationLinks.KnifeMono.UpdateObject(addition);
			_physicsSceneSimulation.SimulatePhysics(addition);
		}
	}

	public interface IPhysicsSceneSimulation
	{
		void SimulatePhysics(float deltaTime);
		Transform RootTransform { get; }
	}
}

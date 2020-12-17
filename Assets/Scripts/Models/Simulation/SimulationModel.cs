using GameSimService;
using UnityEngine;

namespace Models.Simulation
{
	public class SimulationModel : ISimulationModel
	{
		private readonly IPhysicsScene _physicsScene;
		private readonly IAssetsModel _assetsModel;

		private GameObject _instance;
		private SimulationLinks _simulationLinks;
		private GameObject _prefab;

		public SimulationModel(IPhysicsScene physicsScene, IAssetsModel assetsModel)
		{
			_physicsScene = physicsScene;
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
			
			_instance = Object.Instantiate(_prefab, _physicsScene.RootTransform);
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
			if (_simulationLinks == null)
				return;
			
			const float fixedTime = 0.05f;

			int fullIterations = (int) (deltaTime / fixedTime);
			float addition = deltaTime - (fullIterations * fixedTime);
			
			for (int i = 0; i < fullIterations; i++)
			{
				_simulationLinks.KnifeMono.UpdateObject(fixedTime);
				_physicsScene.SimulatePhysics(fixedTime);
			}
			
			_simulationLinks.KnifeMono.UpdateObject(addition);
			_physicsScene.SimulatePhysics(addition);
		}
	}
}

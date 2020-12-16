using UnityEngine;
using Views.SimulationVIew;

namespace Models.Simulation
{
	public class SimulationModel : ISimulationModel
	{
		private readonly IPhysicsScene _physicsScene;
		private readonly IAssetsModel _assetsModel;

		private GameObject _instance;
		private GameObject _prefab;

		public SimulationModel(IPhysicsScene physicsScene, IAssetsModel assetsModel)
		{
			_physicsScene = physicsScene;
			_assetsModel = assetsModel;
		}

		public void InstantiatePrefab()
		{
			_prefab = _assetsModel.Links.SimulationPrefab;
			
			if (_instance != null)
			{
				Object.Destroy(_instance);
				Debug.LogWarning("Loading prefab duplicate");
			}
			
			_instance = Object.Instantiate(_prefab, _physicsScene.RootTransform);
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
			_physicsScene.SimulatePhysics(deltaTime);
		}
	}

	public interface IPhysicsScene
	{
		void SimulatePhysics(float deltaTime);
		Transform RootTransform { get; }
	}
}

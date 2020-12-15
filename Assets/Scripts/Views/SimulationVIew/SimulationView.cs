using UnityEngine;

namespace Views.SimulationVIew
{
	public class SimulationView
	{
		private readonly IScenePhysics _scenePhysics;

		private GameObject _instance;
		private GameObject _prefab;

		public SimulationView(IScenePhysics scenePhysics)
		{
			_scenePhysics = scenePhysics;
		}

		public void InjectLoadedPrefab(GameObject prefab)
		{
			_prefab = prefab;
			
			if (_instance != null)
			{
				Object.Destroy(_instance);
				Debug.LogWarning("Loading prefab duplicate");
			}
			
			_instance = Object.Instantiate(prefab, _scenePhysics.RootTransform);
			_instance.SetActive(false);
		}

		public void Show()
		{
			_instance.SetActive(true);
		}

		public void DestroyGameInstances()
		{
			GameObject.Destroy(_instance);
		}

		public void SimulatePhysics(float deltaTime)
		{
			_scenePhysics.SimulatePhysics(deltaTime);
		}
	}

	public interface IScenePhysics
	{
		void SimulatePhysics(float deltaTime);
		Transform RootTransform { get; }
	}
}

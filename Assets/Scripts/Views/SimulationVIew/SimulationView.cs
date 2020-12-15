using UnityEngine;

namespace Views.SimulationVIew
{
	public class SimulationView
	{
		private readonly IPhysicsScene _physicsScene;

		private GameObject _instance;
		private GameObject _prefab;

		public SimulationView(IPhysicsScene physicsScene)
		{
			_physicsScene = physicsScene;
		}

		public void InstantiatePrefab(GameObject prefab)
		{
			_prefab = prefab;
			
			if (_instance != null)
			{
				Object.Destroy(_instance);
				Debug.LogWarning("Loading prefab duplicate");
			}
			
			_instance = Object.Instantiate(prefab, _physicsScene.RootTransform);
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

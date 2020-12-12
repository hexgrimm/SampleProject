using UnityEngine;
using UnityEngine.SceneManagement;

namespace Views.SimulationVIew
{
	public class SimulationView
	{
		private readonly Scene _scene;
		private readonly ISceneLoader _sceneLoader;

		private GameObject _prefabInstance;
		private PhysicsScene _physicsScene;
		private GameObject _rootObject;

		public SimulationView(Scene scene, ISceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
			_sceneLoader.LoadScene(scene);
			
			_physicsScene = scene.GetPhysicsScene();
			_scene = scene;
			
			ReCreateRootObject();
		}

		public void InjectLoadedPrefab(GameObject prefab)
		{
			if (_prefabInstance != null)
			{
				Object.Destroy(_prefabInstance);
				Debug.LogWarning("Loading prefab duplicate");
			}
			
			_prefabInstance = Object.Instantiate(prefab, _rootObject.transform);
		}

		public void DestroyGameInstances()
		{
			ReCreateRootObject();
		}

		public void SimulatePhysics(float deltaTime)
		{
			_physicsScene.Simulate(deltaTime);
		}

		public void Dispose()
		{
			_sceneLoader.UnloadScene(_scene);
		}

		private void ReCreateRootObject()
		{
			if (_rootObject != null)
			{
				GameObject.Destroy(_rootObject);
			}
			
			_rootObject = new GameObject();
			SceneManager.MoveGameObjectToScene(_rootObject, _scene);
			GameObject.DontDestroyOnLoad(_rootObject);
		}
	}

	public interface ISceneLoader
	{
		void LoadScene(Scene scene);
		void UnloadScene(Scene scene);
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Views.SimulationVIew
{
	public class SimulationView
	{
		private readonly Scene _scene;
		
		private GameObject _prefabInstance;
		private PhysicsScene _physicsScene;
		private GameObject _rootObject;

		public SimulationView(Scene scene)
		{
			var lp = new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.Physics3D);
			SceneManager.LoadScene(scene.name, lp);
			
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
}

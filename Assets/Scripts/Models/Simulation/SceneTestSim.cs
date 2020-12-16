#if UNITY_EDITOR //To use it inside unit test without play mode
using UnityEditor.SceneManagement;
#endif
using Models.Simulation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Views.SimulationVIew
{
	public class PhysicsSceneSimulation : IPhysicsSceneSimulation
	{
		private readonly string _sceneName;
		private Scene _scene;
		private PhysicsScene _physics;

		public PhysicsSceneSimulation(string sceneName)
		{
			_sceneName = sceneName;

			if (Application.isPlaying)
			{
				_scene = SceneManager.CreateScene(_sceneName, new CreateSceneParameters()
				{
					localPhysicsMode = LocalPhysicsMode.Physics3D,
				});
			}
			else
			{
				#if UNITY_EDITOR //To use it inside unit test without play mode
				_scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
				#endif
			}

			_physics = _scene.GetPhysicsScene();

			Physics.autoSimulation = false;
			Physics.autoSyncTransforms = false;
			Physics.gravity = new Vector3(0, -9.81f, 0);
			var go = new GameObject("Simulation-root");
			SceneManager.MoveGameObjectToScene(go, _scene);
			RootTransform = go.transform;
		}

		public void SimulatePhysics(float deltaTime)
		{
			_physics.Simulate(deltaTime);
		}

		public Transform RootTransform { get; }
	}
}
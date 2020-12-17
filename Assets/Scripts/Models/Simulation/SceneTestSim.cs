
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR //To use it inside unit test without play mode
#endif

namespace Models.Simulation
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
			else//test
			{
				#if UNITY_EDITOR //To use it inside unit test without play mode
				_scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
				#endif
			}

			_physics = _scene.GetPhysicsScene();
			
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
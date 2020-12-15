using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Views.SimulationVIew;

namespace Editor.Tests.Simulation
{
	public class SimulationTests
	{
		private SimulationView _view;
		private Scene _scene;

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			_scene = SceneManager.GetSceneByPath("Assets/Scenes/Sim1.unity");
			Assert.IsTrue(_scene.IsValid());
			
			yield break;
		}
		
		[UnityTest]
		public IEnumerator Init()
		{
			//yield return new EnterPlayMode();
			
			
			
			var sl = new SceneTestPhysics();
			
			_view = new SimulationView(_scene, sl);
			yield return new WaitForEndOfFrame();
			
			_view.InjectLoadedPrefab(new GameObject("TEST"));
			for (int i = 0; i < 200; i++)
			{
				_view.SimulatePhysics(0.02f);
				yield return new WaitForSeconds(1f);
			}

			yield return new WaitForSeconds(2.5f);
			
			_view.DestroyGameInstances();
			_view.Dispose();
			//yield return new ExitPlayMode();
		}
		
		private class SceneTestPhysics : IScenePhysics
		{
			public void LoadScene(Scene scene)
			{
				var lp = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);
				SceneManager.LoadScene(scene.name, lp);
			}

			public void UnloadScene(Scene scene)
			{
				SceneManager.UnloadSceneAsync(scene);
			}
		}
	}
}
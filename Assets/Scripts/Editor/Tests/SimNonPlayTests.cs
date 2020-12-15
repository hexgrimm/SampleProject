using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Views.SimulationVIew;

namespace Editor.Tests
{
	[TestFixture]
	public class SimNonPlayTests
	{
		private Scene _scene;

		[Test]
		public void Test1()
		{
			_scene = SceneManager.GetSceneByPath("Assets/Scenes/Sim1.unity");
			Assert.IsTrue(_scene.IsValid());
			
			UnityEditor.EnterPlayModeOptions
			var sl = new SceneTestPhysics();
			
			var view = new SimulationView(_scene, sl);
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
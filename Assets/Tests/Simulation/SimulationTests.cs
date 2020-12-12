using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Views.SimulationVIew;

namespace Editor.Tests.Simulation
{
	[TestFixture]
	public class SimulationTests
	{
		private SimulationView _view;

		[UnityTest]
		public IEnumerable Init()
		{
			var scene = SceneManager.GetSceneByName("SimulationScene");
			var sl = new SceneTestLoader();

			yield return null;
			yield return null;
			_view = new SimulationView(scene, sl);
			_view.SimulatePhysics(0.02f);
			
			yield return null;
			yield return null;
			
			_view.Dispose();
			yield break;
		}
		
		private class SceneTestLoader : ISceneLoader
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
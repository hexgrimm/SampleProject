using NUnit.Framework;
using UnityEngine;
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
			var sl = new PhysicsSceneTestSim("Sim1");
			var view = new SimulationView(sl);
			view.InstantiatePrefab(new GameObject());
			view.Show();
			view.SimulatePhysics(1f);
		}
	}
}
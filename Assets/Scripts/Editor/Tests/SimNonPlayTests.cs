using Models.Simulation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using PhysicsScene = Models.Simulation.PhysicsScene;

namespace Editor.Tests
{
	[TestFixture]
	public class SimNonPlayTests
	{
		private Scene _scene;

		[Test]
		public void Test1()
		{
			var sl = new PhysicsScene("Sim1");
			//var view = new SimulationModel(sl);
			//view.InstantiatePrefab(new GameObject());
			//view.Show();
			//view.SimulatePhysics(1f);
		}
	}
}
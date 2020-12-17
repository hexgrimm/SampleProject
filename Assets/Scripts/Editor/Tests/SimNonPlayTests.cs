using Models.Simulation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor.Tests
{
	[TestFixture]
	public class SimNonPlayTests
	{
		private Scene _scene;

		[Test]
		public void Test1()
		{
			var sl = new PhysicsSceneSimulation("Sim1");
			//var view = new SimulationModel(sl);
			//view.InstantiatePrefab(new GameObject());
			//view.Show();
			//view.SimulatePhysics(1f);
		}
	}
}
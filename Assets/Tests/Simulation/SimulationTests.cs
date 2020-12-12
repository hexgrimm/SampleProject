using NUnit.Framework;
using UnityEngine.SceneManagement;
using Views.SimulationVIew;

namespace Editor.Tests.Simulation
{
	[TestFixture]
	public class SimulationTests
	{
		private SimulationView _view;

		[SetUp]
		public void SetUp()
		{
			_view = new SimulationView(SceneManager.GetSceneByBuildIndex(1));
		}

		[Test]
		public void Init()
		{
			_view.SimulatePhysics(0.02f);
		}
	}
}
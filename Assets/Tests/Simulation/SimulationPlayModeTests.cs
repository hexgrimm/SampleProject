using System.Collections;
using Models;
using Models.Assets;
using Models.Simulation;
using UnityEngine;
using UnityEngine.TestTools;
using PhysicsScene = Models.Simulation.PhysicsScene;

namespace Tests.Simulation
{
	public class SimulationPlayModeTests
	{
		private SimulationModel _simModel;
		private PhysicsScene _physicsSceneSym;

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			_physicsSceneSym = new PhysicsScene("Sim1");
			yield break;
		}
		
		[UnityTest]
		public IEnumerator Init()
		{
			//yield return new EnterPlayMode();

			_simModel = new SimulationModel(_physicsSceneSym, new AssetsModel(new AssetsConfiguration(), new UpdateWatcher("test")));
			yield return new WaitForEndOfFrame();
			
			_simModel.InstantiatePrefab();
			for (int i = 0; i < 4; i++)
			{
				_simModel.Update(0.02f);
				yield return new WaitForSeconds(0.1f);
			}

			yield return new WaitForSeconds(2.5f);
			
			_simModel.DestroyInstance();
			//yield return new ExitPlayMode();
		}
		
		
	}
}
using System.Collections;
using Models;
using Models.AssetsManagement;
using Models.Simulation;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Editor.Tests.Simulation
{
	public class SimulationPlayModeTests
	{
		private SimulationModel _simModel;
		private PhysicsSceneSimulation _physicsSceneSym;

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			_physicsSceneSym = new PhysicsSceneSimulation("Sim1");
			yield break;
		}
		
		[UnityTest]
		public IEnumerator Init()
		{
			//yield return new EnterPlayMode();

			_simModel = new SimulationModel(_physicsSceneSym, new AssetsModel(new AssetLinks(), new UpdateWatcher()));
			yield return new WaitForEndOfFrame();
			
			_simModel.InstantiatePrefab();
			for (int i = 0; i < 4; i++)
			{
				_simModel.Update(0.02f);
				yield return new WaitForSeconds(0.1f);
			}

			yield return new WaitForSeconds(2.5f);
			
			_simModel.DestroyInstanceForUnload();
			//yield return new ExitPlayMode();
		}
		
		
	}
}
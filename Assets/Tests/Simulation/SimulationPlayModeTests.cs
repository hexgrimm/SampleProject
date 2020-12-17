using System.Collections;
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
		private SimulationModel _view;
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

			//_view = new SimulationModel();
			yield return new WaitForEndOfFrame();
			
			//_view.InstantiatePrefab(GameObject.CreatePrimitive(PrimitiveType.Cube));
			for (int i = 0; i < 4; i++)
			{
				_view.Update(0.02f);
				yield return new WaitForSeconds(0.1f);
			}

			yield return new WaitForSeconds(2.5f);
			
			_view.DestroyInstanceForUnload();
			//yield return new ExitPlayMode();
		}
		
		
	}
}
using Models.Assets;
using UnityEngine;

namespace Models.Simulation
{
	public class SimulationModel : ISimulationModel
	{
		private readonly IPhysicsScene _physicsScene;
		private readonly IAssetsModel _assetsModel;

		private GameObject _instance;
		private SimulationLinks _simulationLinks;
		private float _nextBaseTime;
		private float _prevTimeAddition;

		public SimulationModel(IPhysicsScene physicsScene, IAssetsModel assetsModel)
		{
			_physicsScene = physicsScene;
			_assetsModel = assetsModel;
		}

		public void InstantiatePrefab(GameObject prefab)
		{
			if (_instance != null)
			{
				Object.Destroy(_instance);
				Debug.LogWarning("Loading prefab duplicate");
			}
			
			_instance = Object.Instantiate(prefab, _physicsScene.RootTransform);
			_simulationLinks = _instance.GetComponent<SimulationLinks>();
			_instance.SetActive(false);
		}

		public void Show(float startTime)
		{
			_nextBaseTime = startTime;
			_prevTimeAddition = 0;
			
			if (_instance != null)
				_instance.SetActive(true);
		}

		public void Hide()
		{
			if (_instance != null)
				_instance.SetActive(false);
		}

		public void DestroyInstance()
		{
			_simulationLinks = null;
			_prevTimeAddition = 0;
			
			GameObject.Destroy(_instance);
		}

		public void Update(float toTime)
		{
			if (_simulationLinks == null)
				return;

			var delta = toTime - _nextBaseTime + _prevTimeAddition; 
			
			const float fixedTime = 0.03f;

			int fullIterations = (int) (delta / fixedTime);
			_prevTimeAddition =  delta % fixedTime;
			_nextBaseTime = toTime;
			
			for (int i = 0; i < fullIterations; i++)
			{
				_simulationLinks.KnifeMono.UpdateObject(fixedTime);
				_physicsScene.SimulatePhysics(fixedTime);
			}
		}
	}
}

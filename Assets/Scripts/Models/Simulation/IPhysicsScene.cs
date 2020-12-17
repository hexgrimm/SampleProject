using UnityEngine;

namespace Models.Simulation
{
	public interface IPhysicsScene
	{
		void SimulatePhysics(float deltaTime);
		Transform RootTransform { get; }
	}
}
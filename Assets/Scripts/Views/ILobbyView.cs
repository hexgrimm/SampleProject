using System;
using Common;
using UnityEngine;

namespace Views
{
	public interface ILobbyView
	{
		IFlag RequestCoinsButton { get; }
		IFlag StartGameButton { get; }
		
		void ShowOnLayer(int layerIndex, GameObject prefab);
		void Hide();
		void SetCoinsValue(int value);
		void SetDeltaTimeValue(float value);
	}
}
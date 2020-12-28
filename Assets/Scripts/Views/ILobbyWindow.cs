using System;
using Common;
using UnityEngine;

namespace Views
{
	public interface ILobbyWindow : IWindow
	{
		IFlag RequestCoinsButton { get; }
		IFlag StartGameButton { get; }
		
		void SetCoinsValue(int value);
		void SetDeltaTimeValue(float value);
	}
}
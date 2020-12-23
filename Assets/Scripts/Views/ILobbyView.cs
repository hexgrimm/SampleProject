using System;
using Utils;

namespace Views
{
	public interface ILobbyView
	{
		IFlag RequestCoinsButton { get; }
		IFlag StartGameButton { get; }
		
		void ShowOnLayer(int layerIndex);
		void Hide();
		void SetCoinsValue(int value);
		void SetDeltaTimeValue(float value);
	}
}
using System;
using EventUtils;

namespace Views
{
	public interface ILobbyView
	{
		ISignal RequestCoinsButton { get; }
		
		void ShowOnLayer(int layerIndex);
		void Hide();
		void SetCoinsValue(int value);
		void SetDeltaTimeValue(float value);
	}
}
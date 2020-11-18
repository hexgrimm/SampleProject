using System;

namespace Views
{
	public interface ILobbyView : IViewBase
	{
		void SubscribeToRequestButton(Action action);
		void SetCoinsValue(int value);
		void SetDeltaTimeValue(float value);
	}
}
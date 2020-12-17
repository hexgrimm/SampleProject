using EventUtils;

namespace Views
{
	public interface IGameWindow
	{
		ISignal QuitGameButton { get; }
		void ShowOnLayer(int layerIndex);
		void Hide();
	}
}
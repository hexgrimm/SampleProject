using Utils;

namespace Views
{
	public interface IGameWindow
	{
		IFlag QuitGameButton { get; }
		void ShowOnLayer(int layerIndex);
		void Hide();
	}
}
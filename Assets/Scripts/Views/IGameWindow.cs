using Common;

namespace Views
{
	public interface IGameWindow : IWindow
	{
		IFlag QuitGameButton { get; }
	}
}
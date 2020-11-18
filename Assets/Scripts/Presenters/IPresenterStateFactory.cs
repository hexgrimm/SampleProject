namespace Presenters
{
	public interface IPresenterStateFactory
	{
		PresenterStateBase CreateLoadingState();
		PresenterStateBase CreateLobbyState();
	}
}
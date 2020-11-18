using GenericStates;

namespace Presenters
{
	public abstract class PresenterStateBase : State<PresenterStateBase>
	{
		public RootPresenter Context;
		public abstract void OnEnter();
		public abstract void OnExit();
	}
}
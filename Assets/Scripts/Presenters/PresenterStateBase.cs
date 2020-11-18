using GenericStates;

namespace Presenters
{
	public abstract class PresenterStateBase : State<PresenterStateBase>
	{
		public ApplicationPresenter Context;

		public void OnEnter()
		{
			
		}

		public void OnExit()
		{
			
		}
	}
}
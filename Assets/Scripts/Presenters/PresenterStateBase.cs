using System;

namespace Presenters
{
	public abstract class PresenterStateBase
	{
		public Action<PresenterStateBase> SetNewState;
		public IPresenterStateFactory StateFactory;
		public abstract void OnEnter();
		public abstract void OnExit();

		public virtual void PreModelUpdate()
		{
			
		}

		public virtual void PostModelUpdate()
		{
			
		}
	}
}
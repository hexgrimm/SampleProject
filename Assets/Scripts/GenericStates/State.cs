using System;

namespace GenericStates
{
	public abstract class State<T> where T : State<T>
    {
        private T _currentState;
        private Action<T> _setExternalState;

        public T InnerState
        {
            get { return _currentState; }
        }

        protected void BecomeContext()
        {
            _setExternalState = SetStateAsContext;
        }

        protected void SetNewState(T newState)
        {
            if (newState._setExternalState == null)
            {
                newState._setExternalState = _setExternalState;
            }
            
            _setExternalState(newState);
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }


        private void SetStateAsContext(T newState)
        {
            _currentState?.OnExit();

            _currentState = newState;
            newState._setExternalState = _setExternalState;
            _currentState.OnEnter();
        }
    }
}
using DG.Tweening;
using UnityEngine;

namespace Views
{
	public class Loading : ViewBase<LoadingWindowPrefabLinks>, ILoadingWindow
	{
		private Sequence _sequence;
		
		public Loading(Transform parent) : base(parent)
		{
			
		}

		protected override void ShowInternal()
		{
			if (_sequence != null)
			{
				Debug.LogError("HZ");
			}
			else
			{
				_sequence = DOTween.Sequence()
					.Append(PrefabLink.Spinner.transform.DORotate(new Vector3(0, 0, 180), 1))
					.SetLoops(-1);
			}
		}

		protected override void HideInternal()
		{
			if (_sequence != null)
			{
				_sequence.Rewind();
				_sequence.Kill();
				_sequence = null;
			}
		}
	}
}
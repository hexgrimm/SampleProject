using DG.Tweening;
using UnityEngine;

namespace Views
{
	public class LoadingView : ViewBase<LoadingWindowPrefabLinks>, ILoadingWindowView
	{
		private Sequence _sequence;
		
		public LoadingView(Transform parent) : base(parent)
		{
			
		}

		public void EnableSpinnerRotation()
		{
			if (_sequence == null)
			{
				_sequence = DOTween.Sequence()
					.Append(PrefabLink.Spinner.transform.DORotate(new Vector3(0, 0, 90), 1, RotateMode.FastBeyond360))
					.SetLoops(-1);
			}
		}

		protected override void ShowInternal()
		{
			
		}

		protected override void HideInternal()
		{
			if (_sequence != null)
			{
				_sequence.Rewind();
				_sequence.Kill();
			}
		}
	}
}
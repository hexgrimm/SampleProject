using DG.Tweening;
using UnityEngine;

namespace Views
{
	public class LoadingView : ViewBase<LoadingViewPrefabLinks>, ILoadingWindowView
	{
		private Sequence _sequence;
		
		public LoadingView(GameObject prefab, Transform parent) : base(prefab, parent)
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
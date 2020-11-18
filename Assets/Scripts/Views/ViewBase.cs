using UnityEngine;

namespace Views
{
	public class ViewBase<T> : IViewBase where T : MonoBehaviour
	{
		private readonly GameObject _prefab;
		private readonly Transform _parent;
		private GameObject _go;
		protected T PrefabLink;

		public ViewBase(GameObject prefab, Transform parent)
		{
			_prefab = prefab;
			_parent = parent;
		}

		public virtual void Show()
		{
			_go = Object.Instantiate(_prefab, _parent);
			PrefabLink = _go.GetComponent<T>();
		}

		public virtual void Hide()
		{
			GameObject.Destroy(_go);
		}
	}
}
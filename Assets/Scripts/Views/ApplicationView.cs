using UnityEngine;

namespace Views
{
	public class ApplicationView : IApplicationView
	{
		public TView GetView<TView>() //just for example. we can use DI like Zenject to put them to presenter states constructors directly.
		{
			return default(TView);
		}
	}

	public class View<T> where T : MonoBehaviour
	{
		private readonly GameObject _prefab;
		private readonly Transform _parent;
		private GameObject _go;
		protected T PrefabLinks;

		public View(GameObject prefab, Transform parent)
		{
			_prefab = prefab;
			_parent = parent;
		}

		public void Instantiate()
		{
			_go = GameObject.Instantiate(_prefab, _parent);
			PrefabLinks = _go.GetComponent<T>();
			GameObject.DontDestroyOnLoad(_go);
		}

		public void Destroy()
		{
			PrefabLinks = null;
			GameObject.Destroy(_go);
		}
	}
}
using UnityEngine;

namespace Views
{
	public class ViewBase<T> where T : MonoBehaviour
	{
		private readonly GameObject _prefab;
		private readonly Transform _parent;
		protected GameObject GameObjectInstance;
		protected T PrefabLink;

		public ViewBase(GameObject prefab, Transform parent)
		{
			_prefab = prefab;
			_parent = parent;
		}

		protected void Instantiate()
		{
			if (GameObjectInstance != null)
			{
				GameObjectInstance.SetActive(true);
				return;
			}
			
			GameObjectInstance = Object.Instantiate(_prefab, _parent);
			PrefabLink = GameObjectInstance.GetComponent<T>();
		}

		public virtual void Hide()
		{
			if (GameObjectInstance != null)
			{
				GameObjectInstance.SetActive(false);
			}
		}
	}
}
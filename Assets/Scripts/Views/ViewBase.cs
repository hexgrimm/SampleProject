using UnityEngine;

namespace Views
{
	public abstract class ViewBase<T> where T : MonoBehaviour
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

		public void ShowOnLayer(int layerIndex)
		{
			if (GameObjectInstance == null)
			{
				GameObjectInstance = Object.Instantiate(_prefab, _parent);
				PrefabLink = GameObjectInstance.GetComponent<T>();
			}

			GameObjectInstance.SetActive(true);
			GameObjectInstance.transform.SetSiblingIndex(layerIndex);
			ShowInternal();
		}

		public virtual void Hide()
		{
			if (GameObjectInstance != null)
			{
				HideInternal();
				GameObjectInstance.SetActive(false);
			}
		}
		
		protected abstract void ShowInternal();
		protected abstract void HideInternal();
	}
}
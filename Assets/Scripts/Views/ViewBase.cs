using UnityEngine;

namespace Views
{
	public abstract class ViewBase<T> where T : MonoBehaviour
	{
		private readonly Transform _parent;
		protected GameObject GameObjectInstance;
		protected T PrefabLink;

		public ViewBase(Transform parent)
		{
			_parent = parent;
		}

		public void ShowOnLayer(int layerIndex, GameObject prefab)
		{
			if (GameObjectInstance == null)
			{
				GameObjectInstance = Object.Instantiate(prefab, _parent);
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
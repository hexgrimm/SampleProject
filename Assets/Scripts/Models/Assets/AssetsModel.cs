
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Models.Assets
{
	public class AssetsModel : IAssetsModel
	{
		private readonly IUpdateWatcher _updateWatcher;
		public AssetsConfiguration Links { get; }

		public AssetsModel(AssetsConfiguration assetsConfiguration, IUpdateWatcher updateWatcher)
		{
			_updateWatcher = updateWatcher;
			Links = assetsConfiguration;
		}

		public GameObject LoadAsset(ResourceId resourceId)
		{
			#if UNITY_EDITOR
			return AssetDatabase.LoadAssetAtPath<GameObject>(Links.Resources.First(x=>x.ResourceId == resourceId).ResourceLink.PathInAssets);
			#endif
		}

		public void Update()
		{
			_updateWatcher.RegisterUpdate();
		}
	}
}
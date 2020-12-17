#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Models.AssetsManagement
{
	public class AssetsModel : IAssetsModel
	{
		private readonly IUpdateWatcher _updateWatcher;
		public AssetLinks Links { get; }

		public AssetsModel(AssetLinks assetLinks, IUpdateWatcher updateWatcher)
		{
			_updateWatcher = updateWatcher;
			Links = assetLinks;
		}

		public GameObject LoadAsset(string id)
		{
			#if UNITY_EDITOR
			return AssetDatabase.LoadAssetAtPath<GameObject>(Links.SimulationPrefabLink.PathInAssets);
			#endif
		}

		public void Update()
		{
			_updateWatcher.RegisterUpdate();
		}
	}
}
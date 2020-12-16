#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Models.AssetsManagement
{
	public class AssetsModel : IAssetsModel
	{
		public AssetLinks Links { get; }

		public AssetsModel(AssetLinks assetLinks)
		{
			Links = assetLinks;
		}

		public GameObject LoadAsset(string id)
		{
			#if UNITY_EDITOR
			return AssetDatabase.LoadAssetAtPath<GameObject>(id);
			#endif
		}
	}
}
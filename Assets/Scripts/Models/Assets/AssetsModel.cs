
using System.Linq;
using System.Threading.Tasks;
using Common;
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
			var link = Links.Resources.FirstOrDefault(x => x.ResourceId == resourceId);

			if (link == null)
			{
				Debug.LogError("NO resource for ID = " + resourceId);
				return null;
			}

			if (link.ResourceType != ResourceType.InBuild || link.ResourceLink.DirectLink == null)
			{
				Debug.LogError("Cant load synchronously resource for id = " + resourceId);
				return null;
			}
			
			return link.ResourceLink.DirectLink;
		}

		public IPromise<GameObject> LoadAssetAsync(ResourceId resourceId)
		{
			var link = Links.Resources.FirstOrDefault(x => x.ResourceId == resourceId);

			if (link == null)
			{
				var pError = new Promise<GameObject>();
				pError.SetFailed();
				return pError;
			}
			
#if UNITY_EDITOR //TODO: asset bundle support and async load for them

			if (link.ResourceType == ResourceType.InBuild)
			{
				Debug.LogWarning("Loading InBuild resource asynchronous with ID = " + resourceId);
			}

			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(link.ResourceLink.PathInAssets);
			var p = new Promise<GameObject>();
			//emulation of asset bundle async load
			Task.Run(async () =>
			{
				await Task.Delay(3500);
				p.SetComplete(prefab);
			});
			return p;
			
#endif
		}

		public void Update()
		{
			_updateWatcher.RegisterUpdate();
		}
	}
}
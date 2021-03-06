using Common;
using UnityEngine;

namespace Models.Assets
{
	public interface IAssetsModel: IUpdateable
	{
		AssetsConfiguration Links { get; }
		GameObject LoadAsset(ResourceId resourceId);
		IPromise<GameObject> LoadAssetAsync(ResourceId resourceId);
	}
}
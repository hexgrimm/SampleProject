using Common;
using UnityEngine;

namespace Models.Assets
{
	public interface IAssetsModel: IUpdateable
	{
		AssetsConfiguration Links { get; }
		IPromise<GameObject> LoadAsset(ResourceId resourceId);
	}
}
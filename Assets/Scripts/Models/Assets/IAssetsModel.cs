using UnityEngine;

namespace Models.Assets
{
	public interface IAssetsModel: IUpdateable
	{
		AssetLinks Links { get; }
		GameObject LoadAsset(string id);
	}
}
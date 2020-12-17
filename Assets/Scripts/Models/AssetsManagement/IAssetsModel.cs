using Models.AssetsManagement;
using UnityEngine;

namespace Models
{
	public interface IAssetsModel: IUpdateable
	{
		AssetLinks Links { get; }
		GameObject LoadAsset(string id);
	}
}
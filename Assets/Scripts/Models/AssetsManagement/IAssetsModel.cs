using Models.AssetsManagement;
using UnityEngine;

namespace Models
{
	public interface IAssetsModel
	{
		AssetLinks Links { get; }
		GameObject LoadAsset(string id);
	}
}
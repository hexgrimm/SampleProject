using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Models.AssetsManagement
{
	[CreateAssetMenu(fileName = @"Assets\ResourceLinks.asset")]
	public class AssetLinks : ScriptableObject
	{
		public GameObject LoadingWindowPrefab;
		public GameObject LobbyWindowPrefab;
		public GameObject GameWindowPrefab;
		
		[Header("Resource Links:")]
		public ResourceLink SimulationPrefabLink;
	}

	[Serializable]
	public class ResourceLink
	{
		public string PathInAssets;
		public string Id;
	}
}
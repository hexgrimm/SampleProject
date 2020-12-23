using System;
using UnityEngine;

namespace Models.Assets
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

	public class ResourceIdLinkPair
	{
		public ResourcesConfiguration.ViewResourceId ViewId;
		public ResourceLink ResourceLink;
	}
}
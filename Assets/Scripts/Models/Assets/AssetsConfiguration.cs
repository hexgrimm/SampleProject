using System;
using UnityEngine;

namespace Models.Assets
{
	[CreateAssetMenu(fileName = @"Assets\ResourceLinks.asset")]
	public class AssetsConfiguration : ScriptableObject
	{
		public ResourceIdLinkPair[] Resources;
	}

	[Serializable]
	public class ResourceLink
	{
		public GameObject DirectLink;
		public string PathInAssets;
		public string FileName;
	}

	[Serializable]
	public class ResourceIdLinkPair
	{
		public ResourceType ResourceType;
		public ResourceId ResourceId;
		public ResourceLink ResourceLink;
	}
}
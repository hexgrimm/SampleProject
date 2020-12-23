using System;
using System.Collections.Generic;
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
		public string PathInAssets;
		public string Id;
	}

	[Serializable]
	public class ResourceIdLinkPair
	{
		public ResourcesConfiguration.ResourceId Id;
		public ResourceLink ResourceLink;
	}
}
using System;
using UnityEngine;

namespace Models.AssetsManagement
{
	[Serializable]
	public class AssetLinks
	{
		public GameObject CanvasInstance;
		
		[ResourceAddress]
		public string LoadingWindowPrefab;
		[ResourceAddress]
		public string LobbyWindowPrefab;
		[ResourceAddress]
		public string SimulationPrefab;
	}
}
using System.Collections.Generic;
using Common;
using Models.Assets;
using UnityEngine;

namespace Models.Layers
{
	public interface ILayersModel : IUpdateable
	{
		IFlag LayersChanged { get; }
		IReadOnlyList<(ResourceId viewId, GameObject prefab)> Layers { get; }
		
		void ShowViewOnTop(ResourceId viewId, GameObject prefab);
		void HideView(ResourceId viewId);
		void HideAll();
	}
}
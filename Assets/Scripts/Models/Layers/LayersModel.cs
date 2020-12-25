using System.Collections.Generic;
using Common;
using Models.Assets;
using UnityEngine;

namespace Models.Layers
{
	public class LayersModel : ILayersModel
	{
		private readonly IUpdateWatcher _updateWatcher;
		private readonly List<(ResourceId viewId, GameObject prefab)> _layers = new List<(ResourceId, GameObject)>();
		private readonly Flag _layersChanged = new Flag();

		public IFlag LayersChanged => _layersChanged;

		public IReadOnlyList<(ResourceId, GameObject)> Layers => _layers;

		public LayersModel(IUpdateWatcher updateWatcher)
		{
			_updateWatcher = updateWatcher;
		}
		
		public void ShowViewOnTop(ResourceId viewId, GameObject prefab)
		{
			for (int i = 0; i < _layers.Count; i++)
			{
				var item = _layers[i];
				if (item.viewId == viewId)
				{
					_layers.RemoveAt(i);
					break;
				}
			}
			
			_layers.Add((viewId, prefab));
			_layersChanged.Raise();
		}

		public void HideView(ResourceId viewId)
		{
			for (int i = 0; i < _layers.Count; i++)
			{
				var item = _layers[i];
				if (item.viewId == viewId)
				{
					_layers.RemoveAt(i);
					_layersChanged.Raise();
					break;
				}
			}
		}

		public void HideAll()
		{
			_layers.Clear();
			_layersChanged.Raise();
		}

		public void Update()
		{
			_updateWatcher.RegisterUpdate();
		}
	}
}
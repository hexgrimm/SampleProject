using System.Collections.Generic;
using EventUtils;

namespace Models.ViewLayersModel
{
	public class LayersModel : ILayersModel
	{
		private readonly IUpdateWatcher _updateWatcher;
		private readonly List<int> _layers = new List<int>();
		private readonly Signal _layersChanged = new Signal();

		public ISignal LayersChanged => _layersChanged;

		public IReadOnlyList<int> Layers => _layers;

		public LayersModel(IUpdateWatcher updateWatcher)
		{
			_updateWatcher = updateWatcher;
		}
		
		public void ShowViewOnTop(int viewId)
		{
			for (int i = 0; i < _layers.Count; i++)
			{
				var item = _layers[i];
				if (item == viewId)
				{
					_layers.RemoveAt(i);
					break;
				}
			}
			
			_layers.Add(viewId);
			_layersChanged.Raise();
		}

		public void HideView(int viewId)
		{
			for (int i = 0; i < _layers.Count; i++)
			{
				var item = _layers[i];
				if (item == viewId)
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
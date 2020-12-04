using System;
using System.Collections.Generic;

namespace Models.ViewLayersModel
{
	public class ViewLayersModel
	{
		
	}
	
	public interface IViewLayersModel
	{
		event Action LayersChanged;
		IReadOnlyCollection<(int layerIndex, int viewId)> Layers { get; }
		
		void ShowViewOnTop(int viewId);
		void HideView(int viewId);
		void HideAll();
	}
}
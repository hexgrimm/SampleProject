using System.Collections.Generic;
using EventUtils;

namespace Models.ViewLayersModel
{
	public interface ILayersModel : IUpdateable
	{
		ISignal LayersChanged { get; }
		IReadOnlyList<int> Layers { get; }
		
		void ShowViewOnTop(int viewId);
		void HideView(int viewId);
		void HideAll();
	}
}
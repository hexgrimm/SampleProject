using System.Collections.Generic;
using EventUtils;

namespace Models.ApplicationViewModel
{
	public interface IApplicationViewModel
	{
		ISignal LayersChanged { get; }
		IReadOnlyList<int> Layers { get; }

		int Coins { get; }
		int Crystals { get; }
		float DeltaTime { get; }
		
		ISignalSource RequestMoreCoins { get; }
		ISignalSource<int> ExchangeCoinsToCrystals { get; }
	}
}
using System.Collections.Generic;
using EventUtils;

namespace Models.ApplicationViewModel
{
	public interface IRootModel
	{
		//Before Update
		ISignalSource RequestMoreCoins { get; }
		ISignalSource<int> ExchangeCoinsToCrystals { get; }
		void StartNewGame();
		void QuitGame();

		//After Update
		int Coins { get; }
		int Crystals { get; }
		float DeltaTime { get; }
		
		IReadOnlyList<int> Layers { get; }
		ISignal LayersChanged { get; }
		ISignal<bool> GameRunning { get; }
	}
}
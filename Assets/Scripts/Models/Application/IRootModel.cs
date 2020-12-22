using System.Collections.Generic;
using EventUtils;

namespace Models.ApplicationViewModel
{
	public interface IRootModel
	{
		//Before Update
		IFlagHandle RequestMoreCoins { get; }
		IFlagHandle<int> ExchangeCoinsToCrystals { get; }
		void StartNewGame();
		void QuitGame();

		//After Update
		int Coins { get; }
		int Crystals { get; }
		float DeltaTime { get; }
		
		IReadOnlyList<int> Layers { get; }
		IFlag LayersChanged { get; }
		IFlag<bool> GameRunning { get; }
	}
}
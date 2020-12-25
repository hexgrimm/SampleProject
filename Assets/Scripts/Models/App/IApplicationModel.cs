using System.Collections.Generic;
using Common;

namespace Models.App
{
	public interface IApplicationModel : IUpdateable
	{
		//Before Update
		void RequestMoreCoins();
		void ExchangeCoinsToCrystals(int amount);
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
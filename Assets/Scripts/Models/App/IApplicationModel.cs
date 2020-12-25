using System.Collections.Generic;
using Common;
using Models.Assets;
using UnityEngine;

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
		
		IReadOnlyList<(ResourceId viewId, GameObject prefab)> Layers { get; }
		IFlag LayersChanged { get; }
		IFlag<bool> GameRunning { get; }
	}
}